using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBreaker.UI.Shared.Models.Menu;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Menu;
public abstract class CodeBreakerNavMenuBase : ComponentBase
{
    [Parameter, EditorRequired]
    public IEnumerable<NavLinkItem> MenuItems { get; set; } = Enumerable.Empty<NavLinkItem>();
}
