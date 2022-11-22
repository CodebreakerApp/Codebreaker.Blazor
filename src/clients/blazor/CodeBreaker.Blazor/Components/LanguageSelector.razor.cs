using Microsoft.JSInterop;
using System.Globalization;

namespace CodeBreaker.Blazor.Components
{
    public partial class LanguageSelector
    {
        private CultureInfo[] supportedCultures = new[]{new CultureInfo("en"), new CultureInfo("de"), };
        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var js = (IJSInProcessRuntime)JS;
                    js.InvokeVoid("blazorCulture.set", value.Name);
                    Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
                }
            }
        }
    }
}
