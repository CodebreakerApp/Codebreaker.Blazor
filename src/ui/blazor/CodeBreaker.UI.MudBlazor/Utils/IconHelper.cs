using CodeBreaker.UI.Shared.Models.Icon;
using MudBlazor;

namespace CodeBreaker.UI.Utils;
public static class IconHelper
{
    public static string GetIcon(CodeBreakerIcon icon)
    {
        return icon switch
        {
            CodeBreakerIcon.Global => Icons.Material.Filled.Language,
            CodeBreakerIcon.Play => Icons.Material.Filled.PlayCircleOutline,
            CodeBreakerIcon.Cancel => Icons.Material.Filled.CancelPresentation,
            CodeBreakerIcon.Login => Icons.Material.Filled.Login,
            CodeBreakerIcon.Logout => Icons.Material.Filled.Logout,
            CodeBreakerIcon.Info => Icons.Material.Outlined.Info,
            CodeBreakerIcon.Stats => Icons.Material.Outlined.InsertChartOutlined,
            _ => string.Empty
        };
    }
}
