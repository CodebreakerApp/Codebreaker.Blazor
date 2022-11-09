using CodeBreaker.Shared.Models.Api;
using CodeBreaker.Shared.Models.Data.DataGrid;

namespace CodeBreaker.Blazor.Pages
{
    public partial class ReportsPage
    {
        private ILogger? _logger;
        private DateTime? _date = DateTime.Today;
        private List<GameDto>? _games;
        private GameDto? _game;
        private bool _isLoadingGames = false;
        private bool _isLoadingGame = false;

        private readonly List<string> _headers = new()
        {
            "Gamername", "Start", "End", "Number of Moves"
        };

        private List<CodeBreakerColumnDefinition<GameDto>> _columns = new()
        {
            new CodeBreakerColumnDefinition<GameDto>("Gamername", game => game.Username),
            new CodeBreakerColumnDefinition<GameDto>("Start", game => game.Start),
            new CodeBreakerColumnDefinition<GameDto>("End", game => game.End.HasValue ? game.End.Value.ToShortDateString() : string.Empty),
            new CodeBreakerColumnDefinition<GameDto>("Number of Moves", game => game.Moves.Count())
        };
        protected override void OnInitialized()
        {
            _logger = LoggerFactory.CreateLogger<ReportsPage>();
        }

        public async Task GetGames()
        {
            _logger?.LogInformation("Calling GetReport for {date}", _date);
            _games = null;
            _isLoadingGames = true;
            try
            {
                GetGamesResponse? response = await Client.GetGamesAsync(_date);
                _logger?.LogDebug("Got response", response);
                _games = response?.Games?.ToList() ?? new List<GameDto>();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error calling GetGames");
            //throw;
            }
            finally
            {
                _isLoadingGames = false;
            }
        }

        public void HideDetails() => _game = null;
        private async Task ShowDetails(Guid id)
        {
            _logger?.LogInformation("Calling GetDetail for {id}", id);
            try
            {
                GetGameResponse? response = await Client.GetGameAsync(id);
                _game = response?.Game;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error calling GetGame");
                throw;
            }
        }

        private string FormatKeyPegs(CodeBreaker.Shared.Models.Data.KeyPegs? keyPegs) => $"Black: {keyPegs?.Black ?? 'X'} - White: {keyPegs?.White ?? 'X'}";
    }
}
