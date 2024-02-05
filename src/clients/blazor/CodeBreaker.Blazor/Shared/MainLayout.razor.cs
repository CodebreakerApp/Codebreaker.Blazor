using CodeBreaker.UI.Models.Menu;
using CodeBreaker.UI.Models.Icon;
using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.Blazor.Shared;

public partial class MainLayout
{
    private IEnumerable<NavLinkItem> _menuItems = [
        new NavLinkItem("Home", "/", NavLinkMatch.All, CodeBreakerIcon.Dashboard),
        new NavLinkItem("Game", "/game", NavLinkMatch.All, CodeBreakerIcon.Game),
        new NavLinkItem("Reports", "/reports", NavLinkMatch.All, CodeBreakerIcon.Reports),
        new NavLinkItem("About", "/about", NavLinkMatch.All, CodeBreakerIcon.About),
    ];
}
