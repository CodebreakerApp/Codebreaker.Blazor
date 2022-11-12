using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerDatePicker
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;

        [Parameter]
        public DateTime? Date { get; set; }

        [Parameter]
        public EventCallback<DateTime?> DateChanged { get; set; }

        MudDatePicker? _picker;

        private void OnDateChanged(DateTime? date)
        {
            Date = date;
            DateChanged.InvokeAsync(date);
        }
    }
}
