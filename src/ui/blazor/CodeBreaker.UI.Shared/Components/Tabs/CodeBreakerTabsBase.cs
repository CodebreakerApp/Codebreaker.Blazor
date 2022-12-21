using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Tabs;
public abstract class CodeBreakerTabsBase : ComponentBase
{
    [Parameter]
    public string ActiveTab { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ActiveTabChanged { get; set; }

    [Parameter]
    public string[] TabTitles { get; set; } = Array.Empty<string>();

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
