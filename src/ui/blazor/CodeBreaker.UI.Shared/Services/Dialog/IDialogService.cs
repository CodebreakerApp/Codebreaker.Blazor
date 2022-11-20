namespace CodeBreaker.UI.Shared.Services.Dialog;

public record CodeBreakerDialogContext(Type ComponentType, Dictionary<string, object> Parameters, string DialogTitle);

public interface ICodeBreakerDialogService
{
    EventHandler<CodeBreakerDialogContext> ShowDialogHandler { get; set; }
    void ShowDialog(CodeBreakerDialogContext context);
}

public class CodeBreakerDialogService : ICodeBreakerDialogService
{
    public EventHandler<CodeBreakerDialogContext> ShowDialogHandler { get; set; } = default!;

    public void ShowDialog(CodeBreakerDialogContext context)
    {
        ShowDialogHandler.Invoke(this, context);
    }
}
