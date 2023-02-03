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
                CodeBreakerIcon.Global => Icons.Filled.Language,
                CodeBreakerIcon.Play => Icons.Filled.PlayCircleOutline,
                CodeBreakerIcon.Cancel => Icons.Filled.CancelPresentation,
                CodeBreakerIcon.Login => Icons.Filled.Login,
                CodeBreakerIcon.Logout => Icons.Filled.Logout,
                _ => string.Empty
            };

            base.OnInitialized();
        }
    }
}
