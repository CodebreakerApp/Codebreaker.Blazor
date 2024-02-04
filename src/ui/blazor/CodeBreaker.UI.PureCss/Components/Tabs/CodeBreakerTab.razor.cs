using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;
public partial class CodeBreakerTab : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = string.Empty;

    [Parameter]
    public bool Selected { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
