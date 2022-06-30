using CodeBreaker.Shared;
using CodeBreaker.Shared.APIModels;

using System.Net.Http.Json;

namespace CodeBreaker.Blazor.Services;

public class CodebreakerAPIClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    public CodebreakerAPIClient(HttpClient httpclient, ILogger<CodebreakerAPIClient> logger)
    {
        _httpClient = httpclient;
        _logger = logger;
        _logger.LogInformation("Injected HttpClient with base address {uri}", _httpClient.BaseAddress);
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
        string requestUri = "/report";
        if (date is not null)
        {
            requestUri = $"{requestUri}?date={date.Value.ToString("yyyy-MM-dd")}";
        }
        _logger.LogInformation("Calling Codebreaker with {uri}", requestUri);

        return await _httpClient.GetFromJsonAsync<IEnumerable<GamesInfo>>(requestUri);
    }

    //public async Task<GamesInformationDetail?> GetDetailedReportAsync(DateTime? date)
    //{
    //    string requestUri = "/v1/reportdetail";
    //    if (date is not null)
    //    {
    //        requestUri = $"{requestUri}?date={date.Value.ToString("yyyy-MM-dd")}";
    //    }
    //    _logger.LogInformation("Calling Codebreaker with {uri}", requestUri);
        
    //    return await _httpClient.GetFromJsonAsync<GamesInformationDetail>(requestUri);
    //}

    public async Task<CodeBreakerGame?> GetDetailedReportAsync(string id)
    {
        string requestUri = $"/reportdetail/{id}";

        _logger.LogInformation("Calling Codebreaker with {uri}", requestUri);

        return await _httpClient.GetFromJsonAsync<CodeBreakerGame?>(requestUri);
    }
}
