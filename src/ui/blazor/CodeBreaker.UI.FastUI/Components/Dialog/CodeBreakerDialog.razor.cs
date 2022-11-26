using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;

public partial class CodeBreakerDialog : IDisposable
{
    [Inject]
    private ICodeBreakerDialogService _codeBreakerDialogService { get; set; } = default!;

    public bool ModalHidden { get; set; } = true;
    private string _title = string.Empty;
    private RenderFragment? dialogContent;

    protected override void OnInitialized()
    {
        _codeBreakerDialogService.ShowDialogHandler += ShowDialog;
    }

    private void ShowDialog(object? sender, CodeBreakerDialogContext context)
    {
        Console.WriteLine("Show dialog of FastUI");
        _title = context.DialogTitle;
        dialogContent = __builder =>
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
        ModalHidden = false;
        StateHasChanged();
    }

    private void OnCloseModalParameterButtonClick()
    {
        dialogContent = null;
        ModalHidden = true;
        StateHasChanged();
    }
    public void Dispose()
    {
        _codeBreakerDialogService.ShowDialogHandler -= ShowDialog;
    }
}
