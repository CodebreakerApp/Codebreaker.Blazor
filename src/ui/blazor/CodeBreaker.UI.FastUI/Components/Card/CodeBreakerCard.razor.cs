using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI.DesignTokens;

namespace CodeBreaker.UI
{
    public partial class CodeBreakerCard
    {
        [Inject]
        private BaseLayerLuminance _baseLayerLuminanceService { get; set; } = default!;

        private float _currentTheme => _baseLayerLuminanceService.Value ?? 0.98f;
    }
}
