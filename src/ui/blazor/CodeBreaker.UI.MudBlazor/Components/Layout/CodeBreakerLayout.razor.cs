using CodeBreaker.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeBreaker.UI;

public partial class CodeBreakerLayout
{
    [Inject]
    private IThemeService<MudTheme> _themeService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var context = await _themeService.GetCurrentThemeAsync();
        CurrentTheme = context.CurrentTheme;
        IsDark = context.IsDark;
        _themeService.ThemeChanged = OnThemeChanged;
        await base.OnInitializedAsync();
    }

    private void OnThemeChanged(ThemeContext<MudTheme> context)
    {
        IsDark = context.IsDark;
        CurrentTheme = context.CurrentTheme;
        StateHasChanged();
    }
}
