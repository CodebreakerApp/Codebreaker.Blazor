using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
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
        }

        base.OnInitialized();
    }
}
