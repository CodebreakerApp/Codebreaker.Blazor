using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerLayout
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;
        [Parameter]
        public RenderFragment DrawerContent { get; set; } = default!;

        private bool _isDark = false;
        private bool _drawerOpen = true;
        void DrawerToggle() => _drawerOpen = !_drawerOpen;
    }
}
