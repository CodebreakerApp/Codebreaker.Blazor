using CodeBreaker.Blazor.Components;
using CodeBreaker.Blazor.Resources;
using CodeBreaker.Blazor.ViewModels;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.UI.Models.DataGrid;
using CodeBreaker.UI.Services.Dialog;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace CodeBreaker.Blazor.Pages;

public partial class ReportsPage
{
    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    [Inject]
    private IGameReportClient GameClient { get; set; } = default!;

    [Inject]
    private ILogger<ReportsPage> Logger { get; set; } = default!;
    [Inject]
    private IStringLocalizer<Resource> Loc { get; set; } = default!;


    private List<GameDto> _games = [];
    private bool _isLoadingGames = false;
    private readonly ReportFilterContext _filter = new();
    
    private List<string> Headers => [.. Loc.GetString("Reports_Table_Headers").Value.Split(",")];

    private readonly List<CodeBreakerColumnDefinition<GameDto>> _columns = [
        new CodeBreakerColumnDefinition<GameDto>("Gamername", game => game.Username, true),
        new CodeBreakerColumnDefinition<GameDto>("Start", game => game.Start, false),
        new CodeBreakerColumnDefinition<GameDto>("End", game => game.End.HasValue ? game.End.Value : "----", false),
        new CodeBreakerColumnDefinition<GameDto>("Number of Moves", game => game.Moves.Count(), true)
    ];

    public async Task GetGames()
    {
        Logger?.LogInformation("Calling GetReport for {date}", _filter.Date);
        _games = [];
        _isLoadingGames = true;
        try
        {
            GetGamesResponse? response = await GameClient.GetGamesAsync(_filter.Date);
            Logger?.LogDebug("Got response", response);
            _games = [..response?.Games ?? []];
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

    private void ShowReportDialog(GameDto game)
    {
        var title = game.Username;
        if (!game.End.HasValue)
        {
            title = $"{title}: Game was canceled.";
        }
        else if (game.Moves.Any())
        {
            var diff = game.Moves.Last().GuessPegs.Except(game.Code);
            if (diff.Any())
            {
                title = $"{title}: Game was lost.";
            }
            else
            {
                title = $"{title}: Game was won.";
            }
        }

        DialogService.ShowDialog(new DialogContext(typeof(Playground), new Dictionary<string, object>
            {
                { nameof(Playground.Game), game },
                { nameof(Playground.GameFinished), true },
            }, title, []));
    }
}
