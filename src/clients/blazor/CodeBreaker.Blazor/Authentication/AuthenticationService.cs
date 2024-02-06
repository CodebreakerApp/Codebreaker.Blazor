using System.Runtime.CompilerServices;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Services.Authentication.Definitions;
using CodeBreaker.Services.EventArguments;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Identity.Client;

namespace CodeBreaker.Blazor.Authentication;

public class AuthenticationService : IAuthService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IAccessTokenProvider _tokenProvider;

    public UserInformation? LastUserInformation => throw new NotImplementedException();

    private bool _isAuthenticated = false;
    public bool IsAuthenticated => _isAuthenticated;

    public event EventHandler<OnAuthenticationStateChangedEventArgs>? OnAuthenticationStateChanged;

    public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, IAccessTokenProvider tokenProvider)
    {
        _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
        _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        _authenticationStateProvider.AuthenticationStateChanged += AuthenticationStateProvider_AuthenticationStateChanged;
    }

    private async void AuthenticationStateProvider_AuthenticationStateChanged(Task<AuthenticationState> task)
    {
        var state = await task;
        if (state?.User?.Identity is null)
        {
            _isAuthenticated= false;
        } else
        {
            _isAuthenticated = state.User.Identity.IsAuthenticated;
        }

        OnAuthenticationStateChanged?.Invoke(this, new OnAuthenticationStateChangedEventArgs());
    }
    public async Task<AuthenticationResult> AquireTokenAsync(IAuthDefinition authHandler, CancellationToken cancellation = default)
    {
        var tokenResut = await _tokenProvider.RequestAccessToken();
        if (tokenResut is not null && tokenResut.TryGetToken(out var token))
        {
            return new AuthenticationResult(token.Value, true, Guid.NewGuid().ToString(), token.Expires, token.Expires, string.Empty, null, string.Empty, Enumerable.Empty<string>(), Guid.NewGuid());
        }

        return null;
    }
    public Task LogoutAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task RegisterPersistentTokenCacheAsync() => throw new NotImplementedException();
    public Task<bool> TryAquireTokenSilentlyAsync(IAuthDefinition authDefinition, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
