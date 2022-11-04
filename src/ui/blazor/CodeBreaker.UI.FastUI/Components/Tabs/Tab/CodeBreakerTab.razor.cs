using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerTab
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
