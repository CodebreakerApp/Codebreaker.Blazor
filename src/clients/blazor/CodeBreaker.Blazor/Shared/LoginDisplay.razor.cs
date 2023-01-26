using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CodeBreaker.Blazor.Shared
{
    public partial class LoginDisplay
    {
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        public void BeginLogOut()
        {
            _navigationManager.NavigateToLogout("authentication/logout");
        }

        public void BeginLogIn()
        {
            _navigationManager.NavigateToLogout("authentication/login");
        }
    }
}
