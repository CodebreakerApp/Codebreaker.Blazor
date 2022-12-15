using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.Dialogs
{
    public partial class ConfirmDialog
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;

        [Parameter]
        public string Message { get; set; } = string.Empty;
    }
}
