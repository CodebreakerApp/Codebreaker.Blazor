using Microsoft.Fast.Components.FluentUI;
using CodeBreaker.UI.Shared.Models.Icon;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    private string _icon = string.Empty;
    protected override void OnInitialized()
    {
        _icon = Icon switch
        {
            CodeBreakerIcon.Global => FluentIcons.Globe,
            CodeBreakerIcon.Play => FluentIcons.Play,
            CodeBreakerIcon.Cancel => FluentIcons.ShareScreenStop,
            CodeBreakerIcon.Login => FluentIcons.Home,
            CodeBreakerIcon.Logout => FluentIcons.HomePerson,
            _ => string.Empty
        };

        base.OnInitialized();
    }
}
