using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace CodeBreaker.Blazor.Shared
{
    public partial class RedirectToLogin
    {
        [Inject]
        private IOptionsSnapshot<RemoteAuthenticationOptions<ApiAuthorizationProviderOptions>> _options { get; set; } = default!;
        [Inject] NavigationManager _navigationManager { get; set; } = default!;

        protected override void OnInitialized()
        {
            _navigationManager.NavigateToLogin("authentication/login");
        }
    }
}
