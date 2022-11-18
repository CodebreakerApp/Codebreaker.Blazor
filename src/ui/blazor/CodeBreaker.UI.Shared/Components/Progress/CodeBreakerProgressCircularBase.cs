using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Progress;
public abstract class CodeBreakerProgressCircularBase : ComponentBase
{
    [Parameter]
    public bool Indeterminate { get; set; } = true;

    [Parameter]
    public int? Min { get; set; }

    [Parameter]
    public int? Max { get; set; }

    [Parameter]
    public int? Value { get; set; }
}
