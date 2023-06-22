using CodeBreaker.UI.Shared.Models.Icon;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI;
public partial class CodeBreakerNavMenu
{
    private RenderFragment GetIcon(CodeBreakerIcon icon)
    {
        return builder =>
        {
            builder.OpenElement(0, "i");
            switch (icon)
            {
                case CodeBreakerIcon.Cancel:
                    builder.AddAttribute(1, "class", "fa-solid fa-xmark");
                    break;
                case CodeBreakerIcon.Play:
                    builder.AddAttribute(1, "class", "fa-solid fa-play");
                    break;
                case CodeBreakerIcon.Global:
                    builder.AddAttribute(1, "class", "fa-solid fa-language");
                    break;
                case CodeBreakerIcon.Login:
                    builder.AddAttribute(1, "class", "fa-solid fa-arrow-right-to-bracket");
                    break;
                case CodeBreakerIcon.Logout:
                    builder.AddAttribute(1, "class", "fa-solid fa-arrow-right-from-bracket");
                    break;
                case CodeBreakerIcon.Dashboard:
                    builder.AddAttribute(1, "class", "fa-solid fa-house");
                    break;
                case CodeBreakerIcon.Game:
                    builder.AddAttribute(1, "class", "fa-solid fa-chess-board");
                    break;
                case CodeBreakerIcon.Reports:
                    builder.AddAttribute(1, "class", "fa-solid fa-tachograph-digital");
                    break;
                case CodeBreakerIcon.About:
                    builder.AddAttribute(1, "class", "fa-solid fa-address-card");
                    break;

            }

            builder.CloseElement();
        };
    }
}
