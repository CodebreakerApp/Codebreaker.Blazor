using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI.DesignTokens;

namespace CodeBreaker.UI;

public partial class CodeBreakerLayout
{
    [Inject]
    private NavigationManager _navigationManager { get; set; } = default!;
    [Inject]
    private BaseLayerLuminance BaseLayerLuminance { get; set; } = default!;

    
    ElementReference container;
    float baseLayerLuminance = 0.98f;

    protected override void OnInitialized()
    {
        _navigationManager.LocationChanged += (_, __) =>
        {
            DrawerOpen = false;
            StateHasChanged();
        };
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await BaseLayerLuminance.SetValueFor(container, baseLayerLuminance);
    }

    public void SwitchTheme()
    {
        baseLayerLuminance = baseLayerLuminance == 0.15f ? 0.98f : 0.15f;
        IsDark = baseLayerLuminance == 0.15f;
    }
}
