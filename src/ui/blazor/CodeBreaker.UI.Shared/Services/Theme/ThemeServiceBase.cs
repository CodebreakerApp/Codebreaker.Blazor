using Microsoft.JSInterop;

namespace CodeBreaker.UI.Shared.Services.Theme;
public abstract class ThemeServiceBase<T> : IThemeService<T>
{
    private ThemeModule _themeModule;
    protected bool IsDark = false;
    public abstract T DefaultTheme { get; }
    public abstract T DarkTheme { get; }
    public Action<ThemeContext<T>>? ThemeChanged { get; set; }
    public ThemeServiceBase(IJSRuntime jsRuntime)
    {
        if (jsRuntime is null)
        {
            throw new ArgumentNullException(nameof(jsRuntime));
        }

        _themeModule = new ThemeModule(jsRuntime);
    }

    public async Task<ThemeContext<T>> GetCurrentThemeAsync()
    {
        var currentTheme = await _themeModule.GetCurrentThemeAsync();
        IsDark = currentTheme == "dark";
        return IsDark
            ? new ThemeContext<T>(DarkTheme, true)
            : new ThemeContext<T>(DefaultTheme, false);
    }

    public async Task SwitchThemeAsync(bool darkMode)
    {
        IsDark = darkMode;
        ThemeChanged?.Invoke(new ThemeContext<T>(IsDark ? DarkTheme : DefaultTheme, IsDark));
        await _themeModule.PersistThemeAsync(IsDark);
    }
}
