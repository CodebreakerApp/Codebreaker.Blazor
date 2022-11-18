using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Tabs;
public abstract class CodeBreakerTabBase : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
