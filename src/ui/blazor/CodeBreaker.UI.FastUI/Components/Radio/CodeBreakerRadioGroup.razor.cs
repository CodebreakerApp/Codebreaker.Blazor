using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerRadioGroup
    {
        [Parameter]
        public string Value { get; set; } = default!;

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter, EditorRequired]
        public IEnumerable<KeyValuePair<string, string>> Items { get; set; } = default!;


    }
}
