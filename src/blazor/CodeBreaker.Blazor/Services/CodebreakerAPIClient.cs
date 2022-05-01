using CodeBreaker.Shared;
using CodeBreaker.Shared.APIModels;

using System.Net.Http.Json;

namespace CodeBreaker.Blazor.Services;

public class CodebreakerAPIClient
{
    private readonly HttpClient _httpClient;
    public CodebreakerAPIClient(HttpClient httpclient)
    {
        _httpClient = httpclient;
    }

    public async Task<string> StartGameAsync(string name)
    {
        CreateGameRequest request = new(name);
        var response = await _httpClient.PostAsJsonAsync("/start", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<CreateGameResponse>();
        return result.Id;
    }

    public async Task SendMoveAsync(string id, int moveNumber, IEnumerable<string> codePegs)
    {
        MoveRequest request = new(id, moveNumber, codePegs);
        var response = await _httpClient.PostAsJsonAsync("/move", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<MoveResponse>();

    }

    public async Task<IEnumerable<GamesInfo>?> GetReportAsync(DateTime? date)
    {
        string requestUri = "/v1/report";
        if (date is not null)
        {
            requestUri = $"{requestUri}?date={date.Value.ToString("yyyy-MM-dd")}";
        }

        return await _httpClient.GetFromJsonAsync<IEnumerable<GamesInfo>>(requestUri);
    }

    public async Task<GamesInformationDetail?> GetDetailedReportAsync(DateTime? date)
    {
        string requestUri = "/v1/report";
        if (date is not null)
        {
            requestUri = $"{requestUri}?date={date.Value.ToString("yyyy-MM-dd")}";
        }
        return await _httpClient.GetFromJsonAsync<GamesInformationDetail>(requestUri);
    }
}
