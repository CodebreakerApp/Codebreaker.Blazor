namespace CodeBreaker.UI.Services.Dialog;

public class DialogService : IDialogService
{
    public EventHandler<DialogContext> ShowDialogHandler { get; set; } = default!;

    public void ShowDialog(DialogContext context)
    {
        ShowDialogHandler.Invoke(this, context);
    }
}
