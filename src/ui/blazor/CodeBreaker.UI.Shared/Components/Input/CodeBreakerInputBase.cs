using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Input;
public abstract class CodeBreakerInputBase : ComponentBase
{
    [Parameter]
    public string Value { get; set; } = string.Empty;
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }
    [Parameter]
    public bool Required { get; set; } = false;
    [Parameter]
    public string RequiredError { get; set; } = string.Empty;
    [Parameter]
    public string Label { get; set; } = string.Empty;
    [Parameter]
    public string CssClass { get; set; } = string.Empty;
    [Parameter]
    public int MaxLength { get; set; } = int.MaxValue;
}
