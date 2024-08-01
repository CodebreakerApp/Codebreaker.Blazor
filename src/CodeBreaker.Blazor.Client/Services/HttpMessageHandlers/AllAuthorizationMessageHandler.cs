using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace CodeBreaker.Blazor.Client.Services.HttpMessageHandlers;

/// <summary>
/// A <see cref="DelegatingHandler"/> that attaches access tokens to outgoing <see cref="HttpResponseMessage"/> instances.
/// <br />
/// Access tokens will be added to <b>all requests</b>.
/// </summary>
internal class AllAuthorizationMessageHandler : DelegatingHandler, IDisposable
{
    private readonly IAccessTokenProvider _provider;
    private readonly NavigationManager _navigation;
    private readonly AuthenticationStateChangedHandler? _authenticationStateChangedHandler;
    private AccessToken? _lastToken;
    private AuthenticationHeaderValue? _cachedHeader;
    private Uri[]? _authorizedUris;
    private AccessTokenRequestOptions? _tokenOptions;

    public AllAuthorizationMessageHandler(
        IAccessTokenProvider provider,
        NavigationManager navigation)
    {
        _provider = provider;
        _navigation = navigation;

        // Invalidate the cached _lastToken when the authentication state changes
        if (_provider is AuthenticationStateProvider authStateProvider)
        {
            _authenticationStateChangedHandler = _ => { _lastToken = null; };
            authStateProvider.AuthenticationStateChanged += _authenticationStateChangedHandler;
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var now = DateTimeOffset.Now;

        if (_lastToken == null || now >= _lastToken.Expires.AddMinutes(-5))
        {
            var tokenResult = _tokenOptions != null ?
                await _provider.RequestAccessToken(_tokenOptions) :
                await _provider.RequestAccessToken();

            if (tokenResult.TryGetToken(out var token))
            {
                _lastToken = token;
                _cachedHeader = new AuthenticationHeaderValue("Bearer", _lastToken.Value);
            }
            else
            {
                throw new AccessTokenNotAvailableException(_navigation, tokenResult, _tokenOptions?.Scopes);
            }
        }

        // We don't try to handle 401s and retry the request with a new token automatically since that would mean we need to copy the request
        // headers and buffer the body and we expect that the user instead handles the 401s. (Also, we can't really handle all 401s as we might
        // not be able to provision a token without user interaction).
        request.Headers.Authorization = _cachedHeader;

        return await base.SendAsync(request, cancellationToken);
    }

    void IDisposable.Dispose()
    {
        if (_provider is AuthenticationStateProvider authStateProvider)
            authStateProvider.AuthenticationStateChanged -= _authenticationStateChangedHandler;

        Dispose(disposing: true);
    }
}