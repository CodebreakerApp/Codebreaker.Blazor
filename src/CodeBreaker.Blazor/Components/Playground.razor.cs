using Microsoft.AspNetCore.Components;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.Blazor.Pages;
using System.ComponentModel;
using CodeBreaker.Services;
using CodeBreaker.Blazor.Models;
using Microsoft.JSInterop;

namespace CodeBreaker.Blazor.Components;

public partial class Playground
{
    [Inject]
    private IGameClient Client { get; init; } = default!;
    [Inject]
    private IJSRuntime JS { get; init; } = default!;

    [Parameter, EditorRequired]
    public GameDto Game { get; set; } = default!;

    [Parameter]
    public bool GameFinished { get; set; } = false;

    [Parameter]
    public EventCallback<GameMode> GameStatusChanged { get; set; }

    private int MoveNumber => _gameMoves.Count;
    private int OpenMoves => Game.Type.MaxMoves - MoveNumber;
    private bool PlayButtonDisabled =>
        _currentMove.Any(m => string.IsNullOrWhiteSpace(m.Item2) || m.Item2 == "selected" || m.Item2 == "can-drop");
    private string KeyPegsFormat => Game.Type.Holes > 4 ? "three-two" : "two-two";

    private bool _isMobile = false;
    private bool _selectable = false;
    private int _selectedField = -1;
    private readonly BindingList<SelectionAndKeyPegs> _gameMoves = [];
    private string[] _selectionFields = [];
    private List<Tuple<int, string>> _currentMove = [];
    private string _activeColor = string.Empty;
    private IJSInProcessObjectReference? module;


    protected override async Task OnInitializedAsync()
    {
        InitialzePlayground();
        if (Game.Moves.Any())
        {
            foreach (var move in Game.Moves)
            {
                if (move.KeyPegs.HasValue)
                {
                    _gameMoves.Add(new SelectionAndKeyPegs([.. move.GuessPegs], move.KeyPegs.Value, move.MoveNumber));
                }
            }
        }

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender || module is null)
        {
            module = await JS.InvokeAsync<IJSInProcessObjectReference>("import", "./Components/Playground.razor.js");
            _isMobile= await module.InvokeAsync<bool>("isMobile");
            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task SetMoveAsync()
    {
        try
        {
            if (_selectionFields.Length != Game.Type.Holes || _selectionFields.Any(x => x is null || x == string.Empty))
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            var response = await Client.SetMoveAsync(Game.GameId, _selectionFields!);
            _gameMoves.Add(new(_selectionFields!, response.KeyPegs, MoveNumber));

            Console.WriteLine(response.ToString());
            if (response.Won)
            {
                GameFinished = true;
                await GameStatusChanged.InvokeAsync(GameMode.Won);
                StateHasChanged();
            }
            else if (response.Ended)
            {
                GameFinished = true;
                await GameStatusChanged.InvokeAsync(GameMode.Lost);
                StateHasChanged();
            }
            else
            {
                await GameStatusChanged.InvokeAsync(GameMode.MoveSet);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            InitialzePlayground();
        }
    }

    #region ClickEvents
    private void SelectField(int index)
    {
        _selectedField = index;
        for (int i = 0; i < _currentMove.Count; i++)
        {
            var currentClass = _currentMove[i].Item2.Replace("selected", string.Empty).Trim();
            _currentMove[i] = new Tuple<int, string>(i, currentClass);
        }
        _currentMove[_selectedField] = new Tuple<int, string>(_selectedField, $"selected");
        _selectable = true;
    }

    private void SelectColor(string color)
    {
        _selectionFields[_selectedField] = color;
        _currentMove[_selectedField] = new Tuple<int, string>(_selectedField, $"selected {color.ToLower()}");
    }
    #endregion

    #region DragAndDropEvents
    private void UpdateColor(int index)
    {
        _selectionFields[index] = _activeColor;
        _currentMove[index] = new Tuple<int, string>(_selectedField, $"selected {_activeColor.ToLower()}");
    }

    private void SetDropClass(int index)
    {
        for (int i = 0; i < _currentMove.Count; i++)
        {
            _currentMove[i] = new Tuple<int, string>(_currentMove[i].Item1, _currentMove[i].Item2.Replace("can-drop", string.Empty));
        }
        _currentMove[index] = new Tuple<int, string>(_currentMove[index].Item1, $"{_currentMove[index].Item2} can-drop".Trim());
    }

    private void RemoveDropClass()
    {
        for (int i = 0; i < _currentMove.Count; i++)
        {
            _currentMove[i] = new Tuple<int, string>(_currentMove[i].Item1, _currentMove[i].Item2.Replace("can-drop", string.Empty));
        }
    }
    #endregion

    private void InitialzePlayground()
    {
        _selectionFields = new string[Game.Type.Holes];
        _selectedField = -1;
        _currentMove = [];
        for (int i = 0; i < Game.Type.Holes; i++)
        {
            _currentMove.Add(new Tuple<int, string>(i, string.Empty));
        }

    }
}
