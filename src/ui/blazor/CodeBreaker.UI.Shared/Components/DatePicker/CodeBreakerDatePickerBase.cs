using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.DatePicker;
public abstract class CodeBreakerDatePickerBase : ComponentBase
{
    [Parameter]
    public string Label { get; set; } = string.Empty;

    [Parameter]
    public DateTime? Date { get; set; }

    [Parameter]
    public EventCallback<DateTime?> DateChanged { get; set; }
}
