using CodeBreaker.UI.Shared.Models.Icon;

namespace CodeBreaker.UI.Utils;
public static class IconHelper
{
    public static string GetIcon(CodeBreakerIcon icon)
    {
        return icon switch
        {
            CodeBreakerIcon.Global => "bx-globe",
            CodeBreakerIcon.Play => "bx-play-circle",
            CodeBreakerIcon.Cancel => "bx-x",
            CodeBreakerIcon.Login => "bx-log-in-circle",
            CodeBreakerIcon.Logout => "bx-log-out-circle",
            CodeBreakerIcon.Info => "bx-info-circle",
            CodeBreakerIcon.Stats => "bx-bar-chart",
            _ => string.Empty
        };
    }
}
