using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.UI.Components;
public partial class CodeBreakerDatePicker : ComponentBase
{
    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public DateTime? Date { get; set; }

    [Parameter]
    public EventCallback<DateTime?> DateChanged { get; set; }
}
