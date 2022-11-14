namespace CodeBreaker.Blazor.ViewModels;

public class MessageContext
{
    public MessageContext(string message, bool closeable = true)
    {
        // Type = type;
        Message = message;
        Closable = closeable;
    }

    public MessageContext(string message, bool closeable, Action action, string actionText)
    {
        // Type = type;
        Message = message;
        Closable = closeable;
        Action = action;
        ActionText = actionText;
    }

    //public Severity Type { get; set; }

    public string Message { get; set; }

    public bool Closable { get; set; } = true;

    public Action? CloseAction { get; set; }

    public Action? Action { get; set; }

    public string? ActionText { get; set; } = "OK";

    public bool HasAction =>
        Action is not null;

    public void Close() =>
        CloseAction?.Invoke();
}
