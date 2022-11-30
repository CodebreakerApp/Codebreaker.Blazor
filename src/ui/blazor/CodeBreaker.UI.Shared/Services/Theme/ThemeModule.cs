using CodeBreaker.UI.Shared.Services.JavaScript;
using Microsoft.JSInterop;

namespace CodeBreaker.UI.Shared.Services.Theme;
public class ThemeModule : JSModule
{
    public ThemeModule(IJSRuntime js)
        : base(js, "./_content/CodeBreaker.UI.Shared/js/themeService.js")
    {
    }

    public async ValueTask<string> GetCurrentThemeAsync()
        => await InvokeAsync<string>("currentTheme");

    public async ValueTask PersistThemeAsync(bool isDark)
        => await InvokeVoidAsync("switchTheme", isDark);
}
