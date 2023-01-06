using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerThemeSwitch
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;

        private bool _isInitialized = false;
        private IJSObjectReference? _module;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _isInitialized = true;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (_module == null)
                {
                    _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/CodeBreaker.UI/Components/ThemeSwitch/CodeBreakerThemeSwitch.razor.js");
                }
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnSwitchTheme()
        {
            await SwitchTheme(!IsDark);
            if (_module != null)
            {
                await _module.InvokeVoidAsync("switchTheme", IsDark);
            }
        }
    }
}
