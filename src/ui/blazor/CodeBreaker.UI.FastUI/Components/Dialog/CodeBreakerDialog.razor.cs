using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;

public partial class CodeBreakerDialog : IDisposable
{
    [Inject]
    private ICodeBreakerDialogService _codeBreakerDialogService { get; set; } = default!;

    public bool ModalHidden { get; set; } = true;
    private string _title = string.Empty;
    private RenderFragment? _dialogContent;
    private List<CodeBreakerDialogActionContext> _dialogActions = new();

    protected override void OnInitialized()
    {
        _codeBreakerDialogService.ShowDialogHandler += ShowDialog;
    }

    private void ShowDialog(object? sender, CodeBreakerDialogContext context)
    {
        _title = context.DialogTitle;
        _dialogContent = __builder =>
        {
            __builder.OpenComponent(0, context.ComponentType);
            if (context.Parameters?.Count > 0)
            {
                foreach (var param in context.Parameters)
                {
                    __builder.AddAttribute(1, param.Key, param.Value);
                }
            }
            __builder.CloseComponent();
        };
        _dialogActions = context.Actions;
        ModalHidden = false;
        StateHasChanged();
    }

    private void CallAction(Action action)
    {
        action.Invoke();
        CloseDialog();
    }

    private void CloseDialog()
    {
        _dialogContent = null;
        ModalHidden = true;
        StateHasChanged();
    }
    public void Dispose()
    {
        if (_codeBreakerDialogService?.ShowDialogHandler != null)
        {
            _codeBreakerDialogService.ShowDialogHandler -= ShowDialog;
        }
    }
}
