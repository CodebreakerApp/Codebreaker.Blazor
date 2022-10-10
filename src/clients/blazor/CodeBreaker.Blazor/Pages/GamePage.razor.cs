using System.ComponentModel;
using System.Text.Json;
using CodeBreaker.Blazor.Services;
using CodeBreaker.Blazor.ViewModels;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeBreaker.Blazor.Pages;

public enum GameMode
{
    NotRunning,
    Started,
    MoveSet,
    Lost,
    Won
}

public record SelectionAndKeyPegs(string[] GuessPegs, KeyPegsDto KeyPegs, int MoveNumber);

public partial class GamePage
{
    private GameDto? _game;

    [Inject]
    private IGameClient Client { get; init; } = null!;

    private PageMessageService _pageMessageService = new();

    private MudForm? Form { get; set; } = new ();

    public GameMode GameStatus { get; private set; } = GameMode.NotRunning;

    public string Name { get; private set; } = string.Empty;

    public bool InProgress { get; private set; }

    public string?[] SelectionFields { get; private set; } = Array.Empty<string?>();

    public BindingList<SelectionAndKeyPegs> GameMoves { get; private set; } = new ();

    public int MoveNumber => GameMoves.Count;

    public bool CanSetMove =>
        SelectionFields.All(x => x is not null);

    public GameDto? Game
    {
        get => _game;
        private set
        {
            _game = value;
            ClearSelectedColor();
            SelectionFields = new string[value?.Type.Holes ?? 0];
        }
    }

    public async Task StartGameAsync()
    {
        Form?.Validate();

        if (!Form?.IsValid == true)
            return;

        try
        {
            InitializeValues();
            InProgress = true;
            CreateGameResponse response = await Client.StartGameAsync(Name, "6x4Game");
            Game = response.Game;
            GameStatus = GameMode.Started;
        }
        catch (Exception ex)
        {
            _pageMessageService.AddMessage(new (Severity.Error, ex.Message));
        }
        finally
        {
            InProgress = false;
        }
    }

    public async Task SetMoveAsync()
    {
        try
        {
            InProgress = true;

            if (Game is null)
                throw new InvalidOperationException("No game running");

            if (SelectionFields.Length != Game.Value.Type.Holes || SelectionFields.Any(x => x is null || x == string.Empty))
                throw new InvalidOperationException("all colors need to be selected before invoking this method");

            CreateMoveResponse response = await Client.SetMoveAsync(Game.Value.GameId, SelectionFields!);

            SelectionAndKeyPegs selectionAndKeyPegs = new(SelectionFields!, response.KeyPegs, MoveNumber);
            GameMoves.Add(selectionAndKeyPegs);
            GameStatus = GameMode.MoveSet;

            if (response.Won)
            {
                GameStatus = GameMode.Won;
                MessageContext message = new(Severity.Info, "Congratulations - you won", true);
                message.Action = () =>
                {
                    GameStatus = GameMode.NotRunning;
                    message.Close();
                    StateHasChanged();
                };
                message.ActionText = "Continue";
                _pageMessageService.AddMessage(message);
            }
            else if (response.Ended)
            {
                GameStatus = GameMode.Lost;
                MessageContext message = new(Severity.Info, "Sorry, you didn't find the matching colors!", false);
                message.Action = () =>
                {
                    GameStatus = GameMode.NotRunning;
                    message.Close();
                    StateHasChanged();
                };
                message.ActionText = "Continue";
                _pageMessageService.AddMessage(message);
            }
        }
        catch (Exception ex)
        {
            _pageMessageService.AddMessage(new (Severity.Error, ex.Message));
        }
        finally
        {
            ClearSelectedColor();
            InProgress = false;
        }
    }

    private void InitializeValues()
    {
        ClearSelectedColor();
        GameMoves.Clear();
        GameStatus = GameMode.NotRunning;
        
    }

    private void ClearSelectedColor() =>
        SelectionFields = new string[SelectionFields.Length];

    private string GetPegSelectionLabel(int i) =>
        $"Peg {i+1}";
}
