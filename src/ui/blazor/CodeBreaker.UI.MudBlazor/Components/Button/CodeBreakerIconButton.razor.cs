using CodeBreaker.UI.Shared.Models.Icon;
using MudBlazor;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerIconButton
    {
        private string _icon = string.Empty;
        protected override void OnInitialized()
        {
            // TODO: switch expression
            switch(Icon)
            {
                case CodeBreakerIcon.Global:
                    _icon = Icons.Filled.Language;
                    break;
                case CodeBreakerIcon.Play:
                    _icon = Icons.Filled.PlayCircleOutline;
                    break;
                case CodeBreakerIcon.Cancel:
                    _icon = Icons.Filled.CancelPresentation;
                    break;
            }

            base.OnInitialized();
        }
    }
}
