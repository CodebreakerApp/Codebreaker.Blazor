using CodeBreaker.UI.Shared.Models.Menu;
using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.Blazor.Shared
{
    public partial class MainLayout
    {
        private IEnumerable<NavLinkItem> _menuItems = new List<NavLinkItem>
        {
            new NavLinkItem("Home", "/", NavLinkMatch.All, ""),
            new NavLinkItem("Game", "/game", NavLinkMatch.All, ""),
            new NavLinkItem("Reports", "/reports", NavLinkMatch.All, ""),
            new NavLinkItem("About", "/about", NavLinkMatch.All, "")
        };
    }
}
