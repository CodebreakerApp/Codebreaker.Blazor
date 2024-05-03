using CodeBreaker.Blazor.UI.Models.Icon;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.UI.Components;

public partial class CodeBreakerIconButton : CodeBreakerButton
{
    private RenderFragment? _icon;

    [Parameter]
    public CodeBreakerIcon Icon { get; set; }

    protected override void OnInitialized()
    {
        static string? GetClassName(CodeBreakerIcon icon) => icon switch
        {
            CodeBreakerIcon.Cancel => "bi bi-x",
            CodeBreakerIcon.Play => "bi bi-play-fill",
            CodeBreakerIcon.Global => "bi bi-globe",
            CodeBreakerIcon.Login => "bi bi-box-arrow-in-right",
            CodeBreakerIcon.Logout => "bi bi-box-arrow-right",
            CodeBreakerIcon.Dashboard => "bi bi-house",
            CodeBreakerIcon.Game => "bi bi-dice-5",
            CodeBreakerIcon.Reports => "bi bi-table",
            CodeBreakerIcon.About => "bi bi-card-text",
            _ => null
        };
        _icon = builder =>
        {
            builder.OpenElement(0, "i");
            builder.AddAttribute(1, "class", GetClassName(Icon));
            builder.CloseElement();
        };

        base.OnInitialized();
    }
}
