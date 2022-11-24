using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Layout;
public abstract class CodeBreakerLayoutBase : ComponentBase
{
    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public RenderFragment ToolbarContent { get; set; } = default!;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
    [Parameter]
    public RenderFragment DrawerContent { get; set; } = default!;

    protected bool IsDark = false;
    protected bool DrawerOpen = false;
    public void DrawerToggle() => DrawerOpen = !DrawerOpen;
}
