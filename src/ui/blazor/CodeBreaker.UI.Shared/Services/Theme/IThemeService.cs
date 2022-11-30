namespace CodeBreaker.UI.Shared.Services;

public record ThemeContext<T>(T CurrentTheme, bool IsDark);

public interface IThemeService<T>
{
    Action<ThemeContext<T>> ThemeChanged { get; set; }
    Task SwitchThemeAsync(bool darkMode);
    Task<ThemeContext<T>> GetCurrentThemeAsync();
}
