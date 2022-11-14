using CodeBreaker.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerNavMenu
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Parameter, EditorRequired]
        public IEnumerable<NavLinkItem> MenuItems { get; set; } = Enumerable.Empty<NavLinkItem>();

        private string? target;

        protected override void OnInitialized()
        {
            SetTarget(new Uri(NavigationManager.Uri));
            NavigationManager.LocationChanged += LocationChanged;
            base.OnInitialized();
        }

        private void LocationChanged(object? sender, LocationChangedEventArgs e)
        {
            SetTarget(new(e.Location));
            StateHasChanged();
            Console.WriteLine(target);
        }

        private void SetTarget(Uri uri)
        {
            if (uri.Segments.Count() > 1)
                target = uri.Segments[1];
            else
                target = "";
        }

        private Appearance SetAppearance(string location) => (location == $"/{target}") ? Appearance.Neutral : Appearance.Stealth;
    }
}
