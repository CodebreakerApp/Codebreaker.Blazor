using BlazorMM.Shared.APIModels;

using System.Net.Http.Json;

namespace BlazorMM.Client.Services;

public class MMClient
{
    private readonly HttpClient _httpClient;
    public MMClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> StartGameAsync(string name)
    {
        CreateGameRequest request = new(name);
        var response = await _httpClient.PostAsJsonAsync("/api/MasterMind/start", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<CreateGameResponse>();
        return result.Id;
    }
}
