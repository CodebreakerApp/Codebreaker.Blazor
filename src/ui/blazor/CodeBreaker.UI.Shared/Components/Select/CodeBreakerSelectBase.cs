using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Select;
public abstract class CodeBreakerSelectBase<T> : ComponentBase
{
    [Parameter, EditorRequired]
    public Dictionary<string, T> Items { get; set; } = default!;

    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public T Value { get; set; } = default!;

    [Parameter]
    public EventCallback<T> ValueChanged { get; set; } = default!;

    [Parameter]
    public Expression<Func<T>> ValueExpression { get; set; } = default!;
}
