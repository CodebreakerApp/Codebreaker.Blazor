using System.Timers;
using Codebreaker.GameAPIs.Client;
using Codebreaker.GameAPIs.Client.Models;
using CodeBreaker.Blazor.Client.Models;
using CodeBreaker.Blazor.Client.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using System.Collections.Frozen;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using CodeBreaker.Blazor.Client.Contracts.Services;

namespace CodeBreaker.Blazor.Client.Pages;

public partial class GamePage : IDisposable
{
    //TODO: Get Data from API
    private readonly FrozenDictionary<string, GameType> _gameTypes = new Dictionary<string, GameType>() {
        { "6x4", GameType.Game6x4 },
        { "6x4 Mini", GameType.Game6x4Mini },
        { "8x5", GameType.Game8x5 },
        { "5x5x4", GameType.Game5x5x4 },
    }.ToFrozenDictionary();

    private IDisposable? _beforeNavigationInterceptor;
    private readonly System.Timers.Timer _timer = new(TimeSpan.FromHours(1));
    private GameMode _gameStatus = GameMode.NotRunning;
    private string? _gamerName;
    private string[]? _gamerNameSuggestions;
    private bool _loadingGame = false;
    private bool _loadingGamerNameSuggestions = false;
    private bool _isGameCancelling = false;
    private GameInfo? _game;
    private GameType _gameType; // A workaround, because the GameType in GameInfo is a string.
    private bool _isNamePrefilled;
    private PersistingComponentStateSubscription _persistingSubscription;

    [Inject] private ILogger<GamePage> Logger { get; init; } = default!;

    [Inject] private IGamesClient Client { get; init; } = default!;

    [Inject] private IGamerNameSuggestionClient GamerNameSuggestionClient { get; init; } = default!;
    
    [Inject] private NavigationManager NavigationManager { get; init; } = default!;
    
    [Inject] private IDialogService DialogService { get; init; } = default!;

    [Inject] private IStringLocalizer<Resource> Loc { get; init; } = default!;

    [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;

    [Inject] private PersistentComponentState ApplicationState { get; init; } = default!;

    private string? SelectedGameTypeKey { get; set; }

    private GameType? SelectedGameType => SelectedGameTypeKey is null ? null : _gameTypes[SelectedGameTypeKey];

    private bool CanStartGame =>
        !string.IsNullOrWhiteSpace(_gamerName)
        && !_loadingGame
        && SelectedGameType is not null;

    protected override async Task OnInitializedAsync()
    {
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _persistingSubscription = ApplicationState.RegisterOnPersisting(PersistGamerNameAsync);

        // Get gamer name from user claims (if available)
        _gamerName = (await AuthenticationStateTask).User.FindFirst("extension_gamerName")?.Value;
        _isNamePrefilled = !string.IsNullOrEmpty(_gamerName);

        // Get gamer name suggestions, if not prefilled and therefore the user is not authenticated
        if (!_isNamePrefilled && !ApplicationState.TryTakeFromJson(nameof(_gamerNameSuggestions), out _gamerNameSuggestions))
            _gamerNameSuggestions = (await GamerNameSuggestionClient.GetGamerNameSuggestionsAsync()).Suggestions;

        // Set the first suggestion as default, if there are any and therefore the user is not authenticated
        if (_gamerNameSuggestions is not null)
            _gamerName = _gamerNameSuggestions.FirstOrDefault();

        await base.OnInitializedAsync();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            _beforeNavigationInterceptor = NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);

        base.OnAfterRender(firstRender);
    }

    private Task PersistGamerNameAsync()
    {
        ApplicationState.PersistAsJson(nameof(_gamerName), _gamerName);
        ApplicationState.PersistAsJson(nameof(_gamerNameSuggestions), _gamerNameSuggestions);
        return Task.CompletedTask;
    }

    private async Task StartGameAsync()
    {
        if (SelectedGameType is null)
            return;

        try
        {
            _loadingGame = true;
            _gameStatus = GameMode.NotRunning;
            (Guid gameId, int numberCodes, int maxMoves, IDictionary<string, string[]> fieldValues) = await Client.StartGameAsync(SelectedGameType.Value, _gamerName);
            _game = new(gameId, SelectedGameType.Value.ToString(), _gamerName, DateTime.Now, numberCodes, maxMoves)
            {
                FieldValues = fieldValues.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()),
                Codes = []
            };
            _gameType = SelectedGameType.Value;
            _gameStatus = GameMode.Started;
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError(ex, ex.Message);
            DialogService.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
        finally
        {
            _timer.Start();
            _loadingGame = false;
        }
    }

    private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        await InvokeAsync(async () =>
        {
            _timer.Stop();
            _gameStatus = GameMode.Timeout;
            StateHasChanged();
            var dialog = await DialogService.ShowInfoAsync(Loc["GamePage_GameTimedOut_Message"], Loc["GamePage_GameTimedOut_Title"]);
            _ = await dialog.Result;
            _game = null;
            NavigationManager.NavigateTo(string.Empty);
        });
    }

    private async Task GameStatusChanged(GameMode gameMode)
    {
        _timer.Stop();
        _gameStatus = gameMode;
        
        if (_gameStatus is GameMode.Won or GameMode.Lost)
        {
            var dialog = await DialogService.ShowMessageBoxAsync(new DialogParameters<MessageBoxContent>()
            {
                Content = new ()
                {
                    Message = gameMode switch
                    {
                        GameMode.Won => Loc["GamePage_FinishGame_Win"],
                        GameMode.Lost => Loc["GamePage_FinishGame_Lose"],
                        _ => string.Empty
                    },
                },
                PrimaryAction = Loc["GamePage_FinishGame_Ok"],
                SecondaryAction = Loc["GamePage_FinishGame_Restart"],
                PreventDismissOnOverlayClick = true,
            });
            var result = await dialog.Result;

            if (result.Cancelled)
            {
                // Secondary button was clicked
                _game = null;
                _gameStatus = GameMode.NotRunning;
                StateHasChanged();
            }
            else
            {
                // Primary button was clicked
                _game = null;
                NavigationManager.NavigateTo(string.Empty);
            }
        }
        else
        {
            _timer.Start();
        }
    }

    private void CancelGame()
    {
        _timer.Stop();
        _gameStatus = GameMode.Cancelled;
        NavigationManager.NavigateTo(string.Empty);
    }

    private async ValueTask OnLocationChanging(LocationChangingContext context)
    {
        if (_game is null)
            return;

        _isGameCancelling = true;
        var dialog = await DialogService.ShowConfirmationAsync(Loc["GamePage_CancelGame_Info"], Loc["Yes"], Loc["No"], Loc["GamePage_CancelConfirmTitle"]);
        var result = await dialog.Result;

        // Cancel dialog - continue game
        if (result.Cancelled)
            context.PreventNavigation();
        // Confirm dialog - cancel game
        else
            await Client.CancelGameAsync(_game.Id, _game.PlayerName, _gameType);
        
        _isGameCancelling = false;
        StateHasChanged();
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _beforeNavigationInterceptor?.Dispose();
        _persistingSubscription.Dispose();
    }
}
