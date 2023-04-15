using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Radio;
public abstract class CodeBreakerRadioGroupBase<T> : ComponentBase
{
    [Parameter]
    public T Value { get; set; } = default!;

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; }
    [Parameter]
    public Expression<Func<T>> ValueExpression { get; set; }

    [Parameter, EditorRequired]
    public IEnumerable<KeyValuePair<string, T>> Items { get; set; } = default!;

    protected async Task OnValueChanged()
    {
        await ValueChanged.InvokeAsync(Value);
    }
}
