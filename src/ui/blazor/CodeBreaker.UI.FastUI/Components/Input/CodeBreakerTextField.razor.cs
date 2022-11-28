using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerTextField
    {
        private async Task OnValueChangedAsync(ChangeEventArgs args)
        {
            Console.WriteLine($"Value changed: {args.Value}");
            await ValueChanged.InvokeAsync((string)args.Value);
        }
    }
}
