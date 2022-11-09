using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerDatePicker
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;

        [Parameter]
        public DateTime Date { get; set; }

        [Parameter]
        public EventCallback<DateTime> DateChanged { get; set; }

        private string _value = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _value = Date.ToString("d");
            await base.OnInitializedAsync();
        }

        private async Task UpdateDate(ChangeEventArgs args)
        {
            if (args.Value is not null && DateTime.TryParse(args.Value.ToString(), out var newDate))
            {
                await DateChanged.InvokeAsync(newDate);
            }
        }
    }
}
