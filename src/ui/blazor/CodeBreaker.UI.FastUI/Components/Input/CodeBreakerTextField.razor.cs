using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerTextField
    {
        private async Task OnValueChangedAsync()
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }
}
