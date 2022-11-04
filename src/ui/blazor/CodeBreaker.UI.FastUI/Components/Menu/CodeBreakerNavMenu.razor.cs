using CodeBreaker.UI.ViewModels;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerNavMenu
    {
        [Parameter, EditorRequired]
        public IEnumerable<NavLinkItem> MenuItems { get; set; } = Enumerable.Empty<NavLinkItem>();
    }
}
