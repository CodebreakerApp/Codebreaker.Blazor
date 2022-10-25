using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.MudBlazor.Components.Layout
{
    public partial class MudBlazorLayout
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default !;
        [Parameter]
        public RenderFragment DrawerContent { get; set; } = default !;
        private bool _drawerOpen = true;
        void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}
