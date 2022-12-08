using Microsoft.AspNetCore.Components;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.Blazor.Pages;
using System.ComponentModel;
using CodeBreaker.Services;
using System.Text.Json;

namespace CodeBreaker.Blazor.Components;

public partial class Playground
{        
    private string _winTerm = string.Empty;
    private bool _selectable = false;
    private int _selectedField = -1;
    private BindingList<SelectionAndKeyPegs> _gameMoves = new();
    private string[] _selectionFields = Array.Empty<string>();
    private List<Tuple<int, string>> _currentMove = new();

    private int _moveNumber => _gameMoves.Count;
    private int _openMoves => Game.Type.MaxMoves - _moveNumber;
    private bool _playButtonDisabled =>
        _currentMove.Any(m => String.IsNullOrWhiteSpace(m.Item2) || m.Item2 == "selected");
    private string _keyPegsFormat => Game.Type.Holes > 4 ? "three-two" : "two-two";
    private string _activeColor = string.Empty;

    [Inject]
    private IGameClient Client { get; init; } = default!;

    [Parameter, EditorRequired]
    public GameDto Game { get; set; } = default!;

    [Parameter]
    public bool GameFinished { get; set; } = false;

    [Parameter]
    public EventCallback<GameMode> GameStatusChanged { get; set; }

    protected override void OnInitialized()
    {
        Console.WriteLine($"{JsonSerializer.Serialize(Game)}");
        InitialzePlayground();
        if (Game.Moves.Count() > 0)
        {
            foreach (var move in Game.Moves)
            {
                if (move.KeyPegs.HasValue)
                {
                    _gameMoves.Add(new SelectionAndKeyPegs(move.GuessPegs.ToArray(), move.KeyPegs.Value, move.MoveNumber));
                }
            }
        }
        base.OnInitialized();
    }

    public async Task SetMoveAsync()
    {
        try
        {
            if (_selectionFields.Length != Game.Type.Holes || _selectionFields.Any(x => x is null || x == string.Empty))
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            var response = await Client.SetMoveAsync(Game.GameId, _selectionFields!);
            _gameMoves.Add(new(_selectionFields!, response.KeyPegs, _moveNumber));

            Console.WriteLine(response.ToString());
            if (response.Won)
            {
                GameFinished= true;
                _winTerm = "You win the game";
                await GameStatusChanged.InvokeAsync(GameMode.Won);
                StateHasChanged();
            }
            else if (response.Ended)
            {
                GameFinished = true;
                _winTerm = "You lose the game";
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

    private void UpdateColor(int index)
    {
        _selectionFields[index] = _activeColor;
        _currentMove[index] = new Tuple<int, string>(_selectedField, $"selected {_activeColor.ToLower()}");
    }

    private void SetActiveColor(string color)
    {
        _activeColor = color;
    }

    private void InitialzePlayground()
    {
        _selectionFields = new string[Game.Type.Holes];
        _selectedField = -1;
        _selectable = false;
        _currentMove = new List<Tuple<int, string>>();
        for (int i = 0; i < Game.Type.Holes; i++)
        {
            _currentMove.Add(new Tuple<int, string>(i, string.Empty));
        }

    }
}
