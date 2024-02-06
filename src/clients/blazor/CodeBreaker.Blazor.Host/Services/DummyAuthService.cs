using CodeBreaker.Services.Authentication.Definitions;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Services.EventArguments;
using Microsoft.Identity.Client;

namespace CodeBreaker.Blazor.Host.Services;

internal sealed class DummyAuthService : IAuthService
{
    public UserInformation? LastUserInformation { get; }
    public bool IsAuthenticated { get; }

    public event EventHandler<OnAuthenticationStateChangedEventArgs>? OnAuthenticationStateChanged;

    public Task<AuthenticationResult> AquireTokenAsync(IAuthDefinition authHandler, CancellationToken cancellation = default)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RegisterPersistentTokenCacheAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryAquireTokenSilentlyAsync(IAuthDefinition authDefinition, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}