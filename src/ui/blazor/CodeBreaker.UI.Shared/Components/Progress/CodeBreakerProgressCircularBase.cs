using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Progress;
public abstract class CodeBreakerProgressCircularBase<T> : ComponentBase
{
    [Parameter]
    public bool Indeterminate { get; set; } = true;

    [Parameter]
    public T Min { get; set; }

    [Parameter]
    public T Max { get; set; }

    [Parameter]
    public T Value { get; set; }
}
