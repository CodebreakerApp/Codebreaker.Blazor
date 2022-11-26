using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Radio;
public abstract class CodeBreakerRadioGroupBase<T> : ComponentBase
{
    [Parameter]
    public T Value { get; set; } = default!;

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }

    [Parameter, EditorRequired]
    public IEnumerable<KeyValuePair<string, T>> Items { get; set; } = default!;
}
