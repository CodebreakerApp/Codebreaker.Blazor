using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerSwitch
    {
        [Parameter] public bool Value { get; set; }
        [Parameter] public EventCallback<bool> ValueChanged { get; set; }

        private async Task Toggle()
        {
            Value = !Value;
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
