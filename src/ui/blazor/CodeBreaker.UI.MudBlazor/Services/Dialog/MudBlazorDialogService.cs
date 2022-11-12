using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBreaker.Shared.Services.Dialog;
using MudBlazor;

namespace CodeBreaker.UI.Services.Dialog;
public class MudBlazorDialogService : ICodeBreakerDialogService
{
    private readonly IDialogService _dialogService;

    public EventHandler<CodeBreakerDialogContext> ShowDialogHandler { get; set; }

    public MudBlazorDialogService(IDialogService dialogService)
    {
        _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
    }

    public void ShowDialog(CodeBreakerDialogContext context)
    {
        var parameters = new DialogParameters();
        if (context.Parameters?.Count > 0)
        {
            foreach (var parameter in context.Parameters)
            {
                parameters.Add(parameter.Key, parameter.Value);
            }
        }
        _dialogService.Show(context.ComponentType, context.DialogTitle, parameters,
            new DialogOptions { CloseButton = true, DisableBackdropClick = true });
    }
}
