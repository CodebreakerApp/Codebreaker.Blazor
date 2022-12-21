using CodeBreaker.UI.Shared.Services.JavaScript;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerThemeSwitch
    {
        private bool _isInitialized = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _isInitialized = true;
        }

        private async Task OnSwitchTheme()
        {
            await SwitchTheme(!IsDark);
        }
    }
}
