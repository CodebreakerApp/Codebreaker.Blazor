using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CodeBreaker.Blazor.Pages
{
    public partial class Authentication
    {
        [Inject]
        private NavigationManager _navigationManager { get; init; } = default!;

        [Parameter]
        public string Action { get; set; } = string.Empty;

        private void OnLogout()
        {
            _navigationManager.NavigateTo("/", true, true);
        }
    }
}
