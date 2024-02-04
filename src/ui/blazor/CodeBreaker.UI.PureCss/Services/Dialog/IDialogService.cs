namespace CodeBreaker.UI.Services.Dialog;

public record DialogActionContext(string Label, Action Action);
public record DialogContext(Type ComponentType, Dictionary<string, object> Parameters, string DialogTitle, List<DialogActionContext> Actions);

public interface IDialogService
{
    EventHandler<DialogContext> ShowDialogHandler { get; set; }
    void ShowDialog(DialogContext context);
}