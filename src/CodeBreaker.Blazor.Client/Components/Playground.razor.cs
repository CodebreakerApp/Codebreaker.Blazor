using Microsoft.AspNetCore.Components;
using Codebreaker.GameAPIs.Client;
using Codebreaker.GameAPIs.Client.Models;
using CodeBreaker.Blazor.Client.Models;
using CodeBreaker.Blazor.Client.Extensions;
using CodeBreaker.Blazor.Client.Contracts.Services;

namespace CodeBreaker.Blazor.Client.Components;

public record SelectionAndKeyPegs(Field[] GuessPegs, string[] KeyPegs, int MoveNumber);

public partial class Playground
{
    [Inject]
    private IGamesClient Client { get; init; } = default!;

    [Inject]
    private IMobileDetectorService MobileDetectorService { get; set; } = default!;

    [Parameter, EditorRequired]
    public GameInfo Game { get; set; } = default!;

    [Parameter]
    public bool GameFinished { get; set; } = false;

    [Parameter]
    public EventCallback<GameMode> GameStatusChanged { get; set; }

    private int MoveNumber => _gameMoves.Count;

    private int OpenMoves => Game.MaxMoves - MoveNumber;

    private bool PlayButtonDisabled => _currentMove.Any(field =>
           AvailableColors is not null && field.Color is null
        || AvailableShapes is not null && field.Shape is null
    );

    private string KeyPegsFormat => Game.NumberCodes > 4 ? "three-two" : "two-two";

    private IEnumerable<string>? AvailableColors => Game?.FieldValues.GetOrDefault("colors");

    private IEnumerable<string>? AvailableShapes => Game?.FieldValues.GetOrDefault("shapes");

    private bool _isMobile = false;
    private bool _selectable = false;
    private int _selectedField = -1;
    private readonly ICollection<SelectionAndKeyPegs> _gameMoves = [];
    private Field[] _currentMove = [];
    private string? _activeColor;
    private string? _activeShape;

    protected override async Task OnInitializedAsync()
    {
        InitialzePlayground();
        
        // TODO: May be removed, because Playground is not used for finished games anymore.
        foreach (var move in Game.Moves)
            if (move.KeyPegs.Length != 0)
                _gameMoves.Add(new SelectionAndKeyPegs([.. Field.Parse(move.GuessPegs)], move.KeyPegs, move.MoveNumber));

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _isMobile = await MobileDetectorService.IsMobileAsync();

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task SetMoveAsync()
    {
        try
        {
            if (_currentMove.Length != Game.NumberCodes || _currentMove.Any(x => x is null || x.Color == string.Empty))
                throw new InvalidOperationException("All fields need to be selected before invoking this method");

            var serializedFields = _currentMove.Select(field => field.Serialize()).ToArray();
            var response = await Client.SetMoveAsync(Game.Id, Game.PlayerName, Enum.Parse<GameType>(Game.GameType), MoveNumber+1, serializedFields);
            _gameMoves.Add(new(_currentMove, response.Results, MoveNumber));

            Console.WriteLine(response.ToString());
            if (response.IsVictory)
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
        
        for (int i = 0; i < _currentMove.Length; i++)
            _currentMove[i].Selected = false;
        
        _currentMove[_selectedField].Selected = true;
        _selectable = true;
    }

    private void SelectColor(string color)
    {
        _currentMove[_selectedField].Selected = true;
        _currentMove[_selectedField].Color = color;
    }

    private void SelectShape(string shape)
    {
        _currentMove[_selectedField].Selected = true;
        _currentMove[_selectedField].Shape = shape;
    }
    #endregion

    #region DragAndDropEvents
    private void UpdateField(int index)
    {
        _currentMove[index].Selected = true;

        if (_activeColor is not null)
        {
            _currentMove[index].Color = _activeColor;
            _activeColor = null;
        }
        
        if (_activeShape is not null)
        {
            _currentMove[index].Shape = _activeShape;
            _activeShape = null;
        }
    }

    private void SetDropClass(int index)
    {
        for (int i = 0; i < _currentMove.Length; i++)
            _currentMove[i].CanDrop = false;

        _currentMove[index].CanDrop = true;
    }

    private void RemoveDropClass()
    {
        for (int i = 0; i < _currentMove.Length; i++)
            _currentMove[i].CanDrop = false;
    }
    #endregion

    private void InitialzePlayground()
    {
        _selectedField = -1;
        _currentMove = new Field[Game.NumberCodes];
        
        for (int i = 0; i < Game.NumberCodes; i++)
            _currentMove[i] = new ();
    }
}