using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerRadioGroup<T>
    {
        [Parameter]
        public T Value { get; set; } = default!;

        [Parameter]
        public EventCallback<T> ValueChanged { get; set; }

        [Parameter, EditorRequired]
        public IEnumerable<KeyValuePair<string, T>> Items { get; set; } = default!;


    }
}
