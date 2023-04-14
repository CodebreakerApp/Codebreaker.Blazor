using CodeBreaker.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI.DesignTokens;

namespace CodeBreaker.UI;

public partial class CodeBreakerLayout
{
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    [Inject]
    private BaseLayerLuminance _baseLayerLuminanceService { get; set; } = default!;
    [Inject]
    private IThemeService<float> _themeService { get; set; } = default!;


    private ElementReference _container;

    protected override void OnInitialized()
    {
        
        base.OnInitialized();
    }

    protected override async Task OnInitializedAsync()
    {
        var context = await _themeService.GetCurrentThemeAsync();
        SwitchTheme(context);
        _navigationManager.LocationChanged += (_, __) =>
        {
            DrawerOpen = false;
            StateHasChanged();
        };

        _themeService.ThemeChanged =  SwitchTheme;
        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _baseLayerLuminanceService.SetValueFor(_container, CurrentTheme);
    }

    public void SwitchTheme(ThemeContext<float> context)
    {
        Console.WriteLine($"Current thtme changed to: {context.CurrentTheme}");
        CurrentTheme = context.CurrentTheme;
        IsDark = context.IsDark;
        StateHasChanged();
    }
}
