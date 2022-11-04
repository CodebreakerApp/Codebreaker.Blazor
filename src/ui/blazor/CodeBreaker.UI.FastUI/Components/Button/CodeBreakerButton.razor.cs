using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerButton
    {
        [Parameter]
        public bool Disabled { get; set; } = false;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

    }
}
