using CodeBreaker.Blazor.Client.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Globalization;

namespace CodeBreaker.Blazor.Client.Components;

public partial class LanguageSelector
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = default!;

    [Inject]
    private IStringLocalizer<Resource> Loc { get; set; } = default!;

    private Dictionary<string, CultureInfo> _items = [];

    private string? _selectedCultureKey;

    private string? SelectedCultureKey
    {
        get => _selectedCultureKey;
        set
        {
            if (value is null || _selectedCultureKey == value)
                return;

            _selectedCultureKey = value;
            var js = (IJSInProcessRuntime)JsRuntime;
            js.InvokeVoid("blazorCulture.set", _items[value].Name);
            NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        }
    }

    protected override void OnInitialized()
    {
        _items = new() {
            { Loc["Language_English"], new CultureInfo("en") },
            { Loc["Language_German"], new CultureInfo("de") }
        };
        base.OnInitialized();
    }
}
