using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerLayout : ComponentBase, IDisposable
    {
        private bool _isRoot;
        private bool _isDark = false;
        private bool _drawerOpen = false;
        private string? _currentTheme;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        [Parameter]
        public string Label { get; set; } = string.Empty;

        [Parameter]
        public RenderFragment ToolbarContent { get; set; } = default!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter]
        public RenderFragment DrawerContent { get; set; } = default!;

        public void DrawerToggle() => _drawerOpen = !_drawerOpen;

        protected override void OnInitialized()
        {

            _isRoot = IsRootUrl();
            _navigationManager.LocationChanged += _navigationManager_LocationChanged;
            base.OnInitialized();
        }

        private void GoBack()
        {
            _navigationManager.NavigateTo("/");
        }

        private void _navigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            _isRoot = IsRootUrl();
            StateHasChanged();
        }

        private bool IsRootUrl()
        {
            return _navigationManager.Uri == _navigationManager.BaseUri;
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= _navigationManager_LocationChanged;
        }
    }
}
