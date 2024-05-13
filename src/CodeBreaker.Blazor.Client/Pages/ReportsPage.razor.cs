using Codebreaker.GameAPIs.Client;
using Codebreaker.GameAPIs.Client.Models;
using CodeBreaker.Blazor.Client.Components;
using CodeBreaker.Blazor.Client.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;

namespace CodeBreaker.Blazor.Client.Pages;

public partial class ReportsPage
{
    private GameInfo[]? _games;
    private bool _isLoadingGames = false;
    private DateTime? _selectedDate;

    [Inject] private IDialogService DialogService { get; set; } = default!;

    [Inject] private IGamesClient GameClient { get; set; } = default!;

    [Inject] private ILogger<ReportsPage> Logger { get; set; } = default!;

    [Inject] private IStringLocalizer<Resource> Loc { get; set; } = default!;

    private string[] Headers => [.. Loc.GetString("ReportsPage_Table_Headers").Value.Split(",")];

    public async Task GetGamesAsync()
    {
        var date = _selectedDate.ToDateOnly(); ;
        Logger?.LogInformation("Calling GetReport for {date}", date);
        _games = null;
        _isLoadingGames = true;
        try
        {
            var query = new GamesQuery(Date: date, Ended: true);
            var response = await GameClient.GetGamesAsync(query);
            Logger?.LogDebug("Got response: {response}", response);
            _games = [..response ?? []];
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Error calling GetGames");
            //TODO: handle Exception;
        }
        finally
        {
            _isLoadingGames = false;
        }
    }

    private void ShowReportDialog(GameInfo game)
    {
        var title = game.PlayerName;
        if (!game.EndTime.HasValue)
        {
            title = $"{title}: Game was canceled.";
        }
        else if (game.Moves.Any())
        {
            var diff = game.Moves.Last().GuessPegs.Except(game.Codes);
            title = diff.Any()
                ? $"{title}: Game was lost."
                : $"{title}: Game was won.";
        }

        _ = DialogService.ShowDialogAsync<FinishedGameDialog>(game, new DialogParameters()
        {
            Title = title,
            ShowTitle = true,
            ShowDismiss = true,
            PreventDismissOnOverlayClick = false,
        });
    }
}
