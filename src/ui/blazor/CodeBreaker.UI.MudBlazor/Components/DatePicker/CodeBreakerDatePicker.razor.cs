using MudBlazor;

namespace CodeBreaker.UI;

public partial class CodeBreakerDatePicker
{
    MudDatePicker? _picker;

    private void OnDateChanged(DateTime? date)
    {
        Date = date;
        DateChanged.InvokeAsync(date);
    }
}
