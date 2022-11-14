using System.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI.DesignTokens;
using Microsoft.JSInterop;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerLayout
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;
        [Inject]
        private BaseLayerLuminance BaseLayerLuminance { get; set; } = default!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
        [Parameter]
        public RenderFragment DrawerContent { get; set; } = default!;

        private bool _isDark = false;
        private bool _drawerOpen = false;
        void DrawerToggle() => _drawerOpen = !_drawerOpen;

        ElementReference container;
        float baseLayerLuminance = 0.98f;

        protected override void OnInitialized()
        {
            _navigationManager.LocationChanged += (_, __) =>
            {
                _drawerOpen = false;
                StateHasChanged();
            };
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await BaseLayerLuminance.SetValueFor(container, baseLayerLuminance);
        }

        public void SwitchTheme()
        {
            baseLayerLuminance = baseLayerLuminance == 0.15f ? 0.98f : 0.15f;
            _isDark = baseLayerLuminance == 0.15f;
        }
    }
}
