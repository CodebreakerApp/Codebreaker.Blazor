using CodeBreaker.Blazor.Client.Services;

namespace CodeBreaker.Blazor.Client.Contracts.Services;

public interface IGamerNameSuggestionClient
{
    Task<GamerNameSuggestionsResult> GetGamerNameSuggestionsAsync(int count = 10, CancellationToken cancellationToken = default);
}

public record GamerNameSuggestionsResult(string[] Suggestions);