using CodeBreaker.Blazor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Globalization;

namespace CodeBreaker.Blazor.Components
{
    public partial class LanguageSelector
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        [Inject]
        private IJSRuntime _jsRuntime { get; set; } = default!;

        [Inject]
        private IStringLocalizer<Resource> Loc { get; set; } = default!;

        private readonly Dictionary<string, CultureInfo> _items = [];

        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var js = (IJSInProcessRuntime)_jsRuntime;
                    js.InvokeVoid("blazorCulture.set", value.Name);
                    _navigationManager.NavigateTo(_navigationManager.Uri, forceLoad: true);
                }
            }
        }

        protected override void OnInitialized()
        {
            _items.Add(Loc["Language_English"], new CultureInfo("en"));
            _items.Add(Loc["Language_German"], new CultureInfo("de"));
            base.OnInitialized();
        }
    }
}
