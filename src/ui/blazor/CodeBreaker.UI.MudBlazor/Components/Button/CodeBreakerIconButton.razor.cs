using CodeBreaker.UI.Shared.Models.Icon;
using MudBlazor;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerIconButton
    {
        private string _icon = string.Empty;
        protected override void OnInitialized()
        {
            switch(Icon)
            {
                case CodeBreakerIcon.Global:
                    _icon = Icons.Filled.Language;
                    break;
                case CodeBreakerIcon.Play:
                    _icon = Icons.Filled.PlayCircleOutline;
                    break;
            }

            base.OnInitialized();
        }
    }
}
