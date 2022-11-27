using Microsoft.Fast.Components.FluentUI;
using CodeBreaker.UI.Shared.Models.Icon;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    private string _icon = string.Empty;
    protected override void OnInitialized()
    {
        switch (Icon)
        {
            case CodeBreakerIcon.Global:
                _icon = FluentIcons.Globe;
                break;
            case CodeBreakerIcon.Play:
                _icon = FluentIcons.Play;
                break;
            case CodeBreakerIcon.Cancel:
                _icon = FluentIcons.ShareScreenStop;
                break;
        }

        base.OnInitialized();
    }
}
