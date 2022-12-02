using CodeBreaker.UI.Shared.Services.Dialog;
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
        var parameters = new DialogParameters
        {
            { "Context", context }
        };
        _dialogService.Show(typeof(CodeBreakerDialogTemplate), string.Empty, parameters,
            new DialogOptions { CloseButton = true, DisableBackdropClick = true, MaxWidth = MaxWidth.Small });
    }
}
