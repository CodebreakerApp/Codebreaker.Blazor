using System.Net.Http.Json;
using CodeBreaker.Blazor.Client.Contracts.Services;

namespace CodeBreaker.Blazor.Client.Services;

public class GamerNameSuggestionClient(HttpClient httpClient) : IGamerNameSuggestionClient
{
    public async Task<GamerNameSuggestionsResult> GetGamerNameSuggestionsAsync(int count = 10, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"gamer-names/suggestions?count={count}", cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GamerNameSuggestionsResult>(cancellationToken) ?? new ([]);
    }
}