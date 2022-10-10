using CodeBreaker.Blazor.ViewModels;
using MudBlazor;

namespace CodeBreaker.Blazor.Services;

public class PageMessageService
{
    private readonly List<MessageContext> _messages = new ();

    public IReadOnlyList<MessageContext> Messages => _messages;

    public void AddMessage(MessageContext message)
    {
        message.CloseAction = () => _messages.Remove(message);
        _messages.Add(message);
    }
}
