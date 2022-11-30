using CodeBreaker.UI.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.ThemeSwitch;
public abstract class CodeBreakerThemeSwitchBase<T> : ComponentBase
{
    [Inject]
    protected IThemeService<T> _themeService { get; set; } = default!;

    protected bool IsDark = false;

    protected override async Task OnInitializedAsync()
    {
        var context = await _themeService.GetCurrentThemeAsync();
        IsDark = context.IsDark;
        await base.OnInitializedAsync();
    }

    protected async Task SwitchTheme(bool isDark)
    {
        IsDark = isDark;
        await _themeService.SwitchThemeAsync(IsDark);
    }
}
