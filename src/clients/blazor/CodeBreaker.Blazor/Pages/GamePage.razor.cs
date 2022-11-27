using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace CodeBreaker.Blazor.Pages;

public enum GameMode
{
    NotRunning,
    Canceld,
    Started,
    MoveSet,
    Lost,
    Won
}

public record SelectionAndKeyPegs(string[] GuessPegs, KeyPegsDto KeyPegs, int MoveNumber);

public partial class GamePage
{
    private string _selectedGameType = string.Empty;

    //TODO: Get Data from API
    private IEnumerable<KeyValuePair<string, string>> _gameTypes = new List<KeyValuePair<string, string>> {
        new KeyValuePair<string, string>("8x5Game", "8x5Game"),
        new KeyValuePair<string, string>("6x4MiniGame", "6x4MiniGame"),
        new KeyValuePair<string, string>("6x4Game", "6x4Game"),
    };


    [Inject]
    private IGameClient Client { get; init; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; init; } = default!;
    [Inject]
    private IJSRuntime _jSRuntime { get; init; } = default!;

    private GameMode _gameStatus = GameMode.NotRunning;

    private string _name = string.Empty;
    private bool _loadingGame = false;
    private bool _cancelGame = false;
    private GameDto? _game;

    protected override async Task OnInitializedAsync()
    {
        this.NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        await base.OnInitializedAsync();
    }

    public async Task StartGameAsync()
    {
        try
        {
            _loadingGame = true;
            _gameStatus = GameMode.NotRunning;
            CreateGameResponse response = await Client.StartGameAsync(_name, string.IsNullOrWhiteSpace(_selectedGameType) ? "6x4Game" : _selectedGameType);
            _game = response.Game;
            _gameStatus = GameMode.Started;
        }
        catch (Exception ex)
        {
            //TODO: Handle Exception
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _loadingGame = false;
        }
    }

    public void CancelGame()
    {
        _gameStatus = GameMode.Canceld;
        NavigationManager.NavigateTo("");
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        if (_gameStatus is GameMode.Started or GameMode.MoveSet)
        {
            var isConfirmed = await _jSRuntime.InvokeAsync<bool>("confirm", "Do you really want to stop this game?");

            if (!isConfirmed)
            {
                context.PreventNavigation();
            }
        }
    }

    private async ValueTask OnLocationChanging(LocationChangingContext context)
    {
        // TODO: add request if cancel button is not entered.
        if (_game.HasValue)
        {
            _cancelGame = true;
            await Client.CancelGameAsync(_game.Value.GameId);
            _cancelGame = false;
        }
    }
}
