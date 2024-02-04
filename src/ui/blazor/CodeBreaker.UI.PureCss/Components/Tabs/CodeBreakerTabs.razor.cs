using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;

public partial class CodeBreakerTabs : ComponentBase
{
    [Parameter]
    public string ActiveTab { get; set; } = string.Empty;

    [Parameter]
    public EventCallback<string> ActiveTabChanged { get; set; }

    [Parameter]
    public string[] TabTitles { get; set; } = Array.Empty<string>();

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    private async Task ChangeActiveTab(string tab)
    {
        ActiveTab = tab;
        await ActiveTabChanged.InvokeAsync(ActiveTab);
    }
}
