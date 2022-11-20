using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    [Parameter]
    public bool Disabled { get; set; } = false;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }
}
