using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerTabs
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
