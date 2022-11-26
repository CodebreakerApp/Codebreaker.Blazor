using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Progress;
public abstract class CodeBreakerProgressCircularBase<T> : ComponentBase
{
    [Parameter]
    public bool Indeterminate { get; set; } = true;

    [Parameter]
    public T Min { get; set; } = default!;

    [Parameter]
    public T Max { get; set; } = default!;

    [Parameter]
    public T Value { get; set; } = default!;

    [Parameter]
    public string Class { get; set; } = string.Empty;
}
