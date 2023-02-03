using Microsoft.AspNetCore.Components.Web;
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
                    builder.AddAttribute(1, "class", "bx bx-x");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Play:
                    builder.AddAttribute(1, "class", "bx bx-play-circle");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Global:
                    builder.AddAttribute(1, "class", "bx bx-globe");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Login:
                    builder.AddAttribute(1, "class", "bx bx-log-in-circle");
                    break;
                case Shared.Models.Icon.CodeBreakerIcon.Logout:
                    builder.AddAttribute(1, "class", "bx bx-log-out-circle");
                    break;

            }

            builder.CloseElement();
        };
        
        base.OnInitialized();
    }
}
