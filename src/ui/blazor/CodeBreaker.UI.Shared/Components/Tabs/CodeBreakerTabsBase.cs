using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Tabs;
public abstract class CodeBreakerTabsBase : ComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
