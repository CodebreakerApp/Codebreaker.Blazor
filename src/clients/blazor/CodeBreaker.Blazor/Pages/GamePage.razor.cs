using CodeBreaker.Blazor.Dialogs;
using CodeBreaker.Blazor.Resources;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;

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
    private IGameClient _client { get; init; } = default!;
    [Inject]
    private NavigationManager _navigationManager { get; init; } = default!;
    [Inject]
    private ICodeBreakerDialogService _dialogService { get; init; } = default!;

    [Inject]
    private IStringLocalizer<Resource> Loc { get; init; } = default!;

    private GameMode _gameStatus = GameMode.NotRunning;
    private string _name = string.Empty;
    private bool _loadingGame = false;
    private bool _cancelGame = false;
    private GameDto? _game;

    protected override async Task OnInitializedAsync()
    {
        _navigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        await base.OnInitializedAsync();
    }

    public async Task StartGameAsync()
    {
        try
        {
            _loadingGame = true;
            _gameStatus = GameMode.NotRunning;
            CreateGameResponse response = await _client.StartGameAsync(_name, string.IsNullOrWhiteSpace(_selectedGameType) ? "6x4Game" : _selectedGameType);
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

    public void GameStatusChanged(GameMode gameMode)
    {
        _gameStatus = gameMode;
        if (_gameStatus is GameMode.Won or GameMode.Lost)
        {
            _dialogService.ShowDialog(new CodeBreakerDialogContext(typeof(GameResultDialog), new Dictionary<string, object>
            {
                { nameof(GameResultDialog.GameMode), _gameStatus },
                { nameof(GameResultDialog.Username), _name },
            }, string.Empty, new List<CodeBreakerDialogActionContext>
            {
                new CodeBreakerDialogActionContext(Loc["GamePage_FinishGame_Ok"], () => _navigationManager.NavigateTo("")),
                new CodeBreakerDialogActionContext(Loc["GamePage_FinishGame_Restart"], () => RestartGame()),
            }));
        }
    }

    public void CancelGame()
    {
        _gameStatus = GameMode.Canceld;
        _navigationManager.NavigateTo("");
    }

    private void RestartGame()
    {
        _game = null;
        _gameStatus = GameMode.NotRunning;
        StateHasChanged();
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        var cancellationSource = new CancellationTokenSource();
        var task = new Task(context.PreventNavigation, cancellationSource.Token);
        if (_gameStatus is GameMode.MoveSet)
        {
            _dialogService.ShowDialog(new CodeBreakerDialogContext(typeof(ConfirmDialog), new Dictionary<string, object>
            {
                { nameof(ConfirmDialog.Message), "Wollen Sie das Spiel wirklich abbrechen?" },
            }, string.Empty, new List<CodeBreakerDialogActionContext>
            {
                new CodeBreakerDialogActionContext(Loc["Confirm_Cancel"], task.Start),
                new CodeBreakerDialogActionContext(Loc["Confirm_Ok"], cancellationSource.Cancel)
            }));

            await task;
        }
    }

    private async ValueTask OnLocationChanging(LocationChangingContext context)
    {
        if (_game.HasValue)
        {
            _cancelGame = true;
            await _client.CancelGameAsync(_game.Value.GameId);
            _cancelGame = false;
        }
    }
}
