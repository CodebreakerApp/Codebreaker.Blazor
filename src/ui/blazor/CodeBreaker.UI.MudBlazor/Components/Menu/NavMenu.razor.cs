using CodeBreaker.UI.MudBlazor.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.MudBlazor
{
    public partial class NavMenu
    {
        [Parameter, EditorRequired]
        public IEnumerable<NavLinkItem> MenuItems { get; set; } = Enumerable.Empty<NavLinkItem>();
    }
}
