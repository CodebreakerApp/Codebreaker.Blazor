using CodeBreaker.Blazor.Components;
using CodeBreaker.Blazor.ViewModels;
using CodeBreaker.Services;
using CodeBreaker.Shared.Models.Api;
using CodeBreaker.UI.Shared.Models.DataGrid;
using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.Pages
{
    public partial class ReportsPage
    {
        [Inject]
        private ICodeBreakerDialogService _codeBreakerDialogService { get; set; } = default!;

        [Inject]
        private IGameReportClient _gameClient { get; set; } = default!;

        [Inject]
        private ILogger<ReportsPage> _logger { get; set; } = default!;


        private List<GameDto> _games = new();
        private bool _isLoadingGames = false;
        private ReportFilterContext _filter = new();

        private readonly List<string> _headers = new()
        {
            "Gamername", "Start", "End", "Number of Moves"
        };

        private List<CodeBreakerColumnDefinition<GameDto>> _columns = new()
        {
            new CodeBreakerColumnDefinition<GameDto>("Gamername", game => game.Username),
            new CodeBreakerColumnDefinition<GameDto>("Start", game => game.Start),
            new CodeBreakerColumnDefinition<GameDto>("End", game => game.End.HasValue ? game.End.Value : "----"),
            new CodeBreakerColumnDefinition<GameDto>("Number of Moves", game => game.Moves.Count())
        };

        public async Task GetGames()
        {
            _logger?.LogInformation("Calling GetReport for {date}", _filter.Date);
            _games = new List<GameDto>();
            _isLoadingGames = true;
            try
            {
                GetGamesResponse? response = await _gameClient.GetGamesAsync(_filter.Date);
                _logger?.LogDebug("Got response", response);
                _games = response?.Games?.ToList() ?? new List<GameDto>();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error calling GetGames");
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

            _codeBreakerDialogService.ShowDialog(new CodeBreakerDialogContext(typeof(Playground), new Dictionary<string, object>
                {
                    { nameof(Playground.Game), game },
                    { nameof(Playground.GameAlreadyFinished), true },
                }, title));
        }
    }
}
