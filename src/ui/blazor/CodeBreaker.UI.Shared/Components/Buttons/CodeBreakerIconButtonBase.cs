using CodeBreaker.UI.Shared.Models.Icon;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Buttons;
public abstract class CodeBreakerIconButtonBase : CodeBreakerButtonBase
{
    [Parameter]
    public CodeBreakerIcon Icon { get; set; }

    protected string _icon = string.Empty;
    protected override void OnInitialized()
    {
        _icon = GetIcon();
        base.OnInitialized();
    }

    public abstract string GetIcon();
}
