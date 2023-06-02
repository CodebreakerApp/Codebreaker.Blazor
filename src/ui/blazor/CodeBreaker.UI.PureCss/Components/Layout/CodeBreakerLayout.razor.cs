using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerLayout : IDisposable
    {
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        private bool _isRoot;

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
