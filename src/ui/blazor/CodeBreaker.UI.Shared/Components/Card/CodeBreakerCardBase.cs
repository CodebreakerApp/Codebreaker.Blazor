using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Card;
public abstract class CodeBreakerCardBase : ComponentBase
{
    [Parameter]
    public RenderFragment HeaderContent { get; set; } = default!;

    [Parameter]
    public RenderFragment HeaderActions { get; set; } = default!;

    [Parameter]
    public RenderFragment CardContent { get; set; } = default!;

    [Parameter]
    public RenderFragment CardActions { get; set; } = default!;

    [Parameter]
    public string Class { get; set; } = string.Empty;
}
