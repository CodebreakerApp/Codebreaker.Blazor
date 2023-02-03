using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeBreaker.Blazor.Authentication;

public class CodeBreakerAuthorizationMessageHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider;

    public CodeBreakerAuthorizationMessageHandler(IAccessTokenProvider accessor)
    {
        _tokenProvider = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessTokenResult = await _tokenProvider.RequestAccessToken();
        if (accessTokenResult.TryGetToken(out var accessToken) && !string.IsNullOrWhiteSpace(accessToken.Value))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
        }
        Console.WriteLine(JsonSerializer.Serialize(accessTokenResult));
        return await base.SendAsync(request, cancellationToken);
    }
}
