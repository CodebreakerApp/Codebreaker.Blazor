using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerDialogTemplate
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; } = default!;

        [Parameter]
        public CodeBreakerDialogContext Context { get; set; } = default!;

        private RenderFragment?_dialogContent;

        void Close() => MudDialog.Close(DialogResult.Ok(true));

        protected override void OnInitialized()
        {
            _dialogContent = __builder =>
            {
                __builder.OpenComponent(0, Context.ComponentType);
                if (Context.Parameters?.Count > 0)
                {
                    foreach (var param in Context.Parameters)
                    {
                        __builder.AddAttribute(1, param.Key, param.Value);
                    }
                }
                __builder.CloseComponent();
            };
            base.OnInitialized();
        }

        private void CallAction(Action? action)
        {
            Close();
            action?.Invoke();
        }
    }
}
