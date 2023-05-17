using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    private RenderFragment? _icon;

    protected override void OnInitialized()
    {
        _icon = builder =>
        {
            builder.OpenElement(0, "i");
            switch (Icon)
            {
                case Shared.Models.Icon.CodeBreakerIcon.Cancel:
                    builder.AddAttribute(1, "class", "fa-solid fa-xmark");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Play:
                    builder.AddAttribute(1, "class", "fa-solid fa-play");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Global:
                    builder.AddAttribute(1, "class", "fa-solid fa-language");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Login:
                    builder.AddAttribute(1, "class", "fa-solid fa-arrow-right-to-bracket");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Logout:
                    builder.AddAttribute(1, "class", "fa-solid fa-arrow-right-from-bracket");
                    break;

            }

            builder.CloseElement();
        };
        
        base.OnInitialized();
    }
}
