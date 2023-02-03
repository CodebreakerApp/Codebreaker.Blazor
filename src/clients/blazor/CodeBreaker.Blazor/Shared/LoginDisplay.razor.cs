using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Blazor.Shared
{
    public partial class LoginDisplay
    {
        [Inject] private NavigationManager _navigationManager { get; set; } = default!; 
        [Inject] private IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> _options { get; set; } = default!;

        public async Task BeginLogOut()
        {
            _navigationManager.NavigateToLogout(_options.Get(Options.DefaultName)
                .AuthenticationPaths.LogOutPath);
        }

        public void BeginLogIn()
        {
            _navigationManager.NavigateToLogin(_options.Get(Options.DefaultName)
                .AuthenticationPaths.LogInPath);
        }
    }
}
