using CodeBreaker.UI.ViewModels;
using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.Blazor.Shared
{
    public partial class MainLayout
    {
        private IEnumerable<NavLinkItem> _menuItems = new List<NavLinkItem>
        {
            new NavLinkItem("Home", "/", NavLinkMatch.All, MudBlazor.Icons.Material.Filled.Home),
            new NavLinkItem("Game", "/game", NavLinkMatch.All, MudBlazor.Icons.Material.Filled.Gamepad),
            new NavLinkItem("Reports", "/reports", NavLinkMatch.All, MudBlazor.Icons.Material.Filled.ViewList),
            new NavLinkItem("About", "/about", NavLinkMatch.All, MudBlazor.Icons.Material.Filled.Info)
        };
    }
}
