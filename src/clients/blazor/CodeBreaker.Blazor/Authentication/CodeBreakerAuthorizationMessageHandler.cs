using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.Authentication;

public class CodeBreakerAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CodeBreakerAuthorizationMessageHandler(IConfiguration config, IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
           authorizedUrls: new[] { config["ApiBase"] });

    }
}
