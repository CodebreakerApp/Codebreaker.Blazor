using CodeBreaker.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;

namespace CodeBreaker.UI;

public partial class CodeBreakerLayout
{
    [Inject]
    private IThemeService<MudTheme> _themeService { get; set; } = default!;

    [Inject]
    private IBreakpointService _breakpointService { get; set; } = default!;

    private BreakpointServiceSubscribeResult? _serviceSubscribeResult;
    private Breakpoint _currentBreakpoint = Breakpoint.None;

    protected override async Task OnInitializedAsync()
    {
        var context = await _themeService.GetCurrentThemeAsync();
        CurrentTheme = context.CurrentTheme;
        IsDark = context.IsDark;
        _themeService.ThemeChanged = OnThemeChanged;
        _currentBreakpoint = await _breakpointService.GetBreakpoint();
        _serviceSubscribeResult = await _breakpointService.Subscribe(breakpoint =>
        {
            _currentBreakpoint = breakpoint;
            StateHasChanged();
        });
        await base.OnInitializedAsync();
    }

    private void OnThemeChanged(ThemeContext<MudTheme> context)
    {
        IsDark = context.IsDark;
        CurrentTheme = context.CurrentTheme;
        StateHasChanged();
    }
}
