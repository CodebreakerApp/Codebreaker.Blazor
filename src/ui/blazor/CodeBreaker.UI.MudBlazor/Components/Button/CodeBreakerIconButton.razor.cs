using CodeBreaker.UI.Shared.Models.Icon;
using MudBlazor;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerIconButton
    {
        private string _icon = string.Empty;
        protected override void OnInitialized()
        {
            _icon = Icon switch
            {
                CodeBreakerIcon.Global => Icons.Material.Filled.Language,
                CodeBreakerIcon.Play => Icons.Material.Filled.PlayCircleOutline,
                CodeBreakerIcon.Cancel => Icons.Material.Filled.CancelPresentation,
                CodeBreakerIcon.Login => Icons.Material.Filled.Login,
                CodeBreakerIcon.Logout => Icons.Material.Filled.Logout,
                CodeBreakerIcon.Dashboard => Icons.Material.Filled.Dashboard,
                CodeBreakerIcon.Reports => Icons.Material.Filled.Report,
                CodeBreakerIcon.Game => Icons.Material.Filled.Games,
                CodeBreakerIcon.About => Icons.Material.Filled.PersonOutline,
                _ => string.Empty
            };

            base.OnInitialized();
        }
    }
}
