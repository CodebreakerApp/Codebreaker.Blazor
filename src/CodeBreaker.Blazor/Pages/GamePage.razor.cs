﻿using System.Timers;
using CodeBreaker.Blazor.Components;
using CodeBreaker.Blazor.Models;
using CodeBreaker.Blazor.Resources;
using CodeBreaker.Blazor.UI.Services.Dialog;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace CodeBreaker.Blazor.Pages;

public record SelectionAndKeyPegs(string[] GuessPegs, KeyPegsDto KeyPegs, int MoveNumber);

public partial class GamePage : IDisposable
{
    private string _selectedGameType = "6x4Game";

    //TODO: Get Data from API
    private readonly IEnumerable<KeyValuePair<string, string>> _gameTypes = [
        new KeyValuePair<string, string>("8x5Game", "8x5Game"),
        new KeyValuePair<string, string>("6x4MiniGame", "6x4MiniGame"),
        new KeyValuePair<string, string>("6x4Game", "6x4Game"),
    ];

    [Inject]
    private IGameClient Client { get; init; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; init; } = default!;
    [Inject]
    private IJSRuntime JSRuntime { get; init; } = default!;
    [Inject]
    private IDialogService DialogService { get; init; } = default!;
    [Inject]
    private IStringLocalizer<Resource> Loc { get; init; } = default!;
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; init; } = default!;

    private readonly System.Timers.Timer _timer = new(TimeSpan.FromHours(1));
    private GameMode _gameStatus = GameMode.NotRunning;
    private string _name = string.Empty;
    private bool _loadingGame = false;
    private bool _cancelGame = false;
    private GameDto? _game;

    protected override async Task OnInitializedAsync()
    {
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        NavigationManager.RegisterLocationChangingHandler(OnLocationChanging);
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        _name = string.IsNullOrWhiteSpace(state?.User?.Identity?.Name)
            ? string.Empty
            : state.User.Identity.Name;
        await base.OnInitializedAsync();
    }

    private async Task StartGameAsync()
    {
        try
        {
            _loadingGame = true;
            _gameStatus = GameMode.NotRunning;
            var response = await Client.StartGameAsync(_name,
                string.IsNullOrWhiteSpace(_selectedGameType) ? "6x4Game" : _selectedGameType);
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
            _timer.Start();
            _loadingGame = false;
        }
    }

    private async void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        await InvokeAsync(() =>
        {
            //TODO: Show dialog
            Console.WriteLine($"Time out called...Cancel game. Time {e.SignalTime}");
            _timer.Stop();
            _gameStatus = GameMode.Cancelled;
            StateHasChanged();
            DialogService.ShowDialog(new DialogContext(typeof(GameResultDialog), new Dictionary<string, object>
            {
                { nameof(GameResultDialog.GameMode), GameMode.Timeout },
                { nameof(GameResultDialog.Username), _name },
            }, string.Empty, [
                new DialogActionContext(Loc["GamePage_FinishGame_Ok"], () => NavigationManager.NavigateTo("")),
                new DialogActionContext(Loc["GamePage_FinishGame_Restart"], () => RestartGame()),
            ]));
        });
    }

    private void GameStatusChanged(GameMode gameMode)
    {
        _timer.Stop();
        _gameStatus = gameMode;
        if (_gameStatus is GameMode.Won or GameMode.Lost)
        {
            DialogService.ShowDialog(new DialogContext(typeof(GameResultDialog), new Dictionary<string, object>
            {
                { nameof(GameResultDialog.GameMode), _gameStatus },
                { nameof(GameResultDialog.Username), _name },
            }, string.Empty, [
                new DialogActionContext(Loc["GamePage_FinishGame_Ok"], () => NavigationManager.NavigateTo(string.Empty)),
                new DialogActionContext(Loc["GamePage_FinishGame_Restart"], () => RestartGame()),
            ]));
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
        NavigationManager.NavigateTo("");
    }

    private void RestartGame()
    {
        _game = null;
        _gameStatus = GameMode.NotRunning;
        StateHasChanged();
    }

    private async Task OnBeforeInternalNavigation(LocationChangingContext context)
    {
        if (_gameStatus is GameMode.MoveSet)
        {
            var isConfirmed = await JSRuntime.InvokeAsync<bool>("confirm", Loc["GamePage_CancelGame_Info"]);

            if (!isConfirmed)
            {
                context.PreventNavigation();
            }
        }
    }

    private async ValueTask OnLocationChanging(LocationChangingContext context)
    {
        if (_game.HasValue)
        {
            _cancelGame = true;
            await Client.CancelGameAsync(_game.Value.GameId);
            _cancelGame = false;
        }
    }

    public void Dispose() => _timer?.Dispose();
}
