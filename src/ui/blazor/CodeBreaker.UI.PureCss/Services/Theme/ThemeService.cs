using CodeBreaker.UI.Shared.Services.Theme;
using Microsoft.JSInterop;

namespace CodeBreaker.UI.Services.Theme;

public class ThemeService : ThemeServiceBase<float>
{
    public ThemeService(IJSRuntime jsRuntime) : base(jsRuntime)
    {
    }

    public override float DefaultTheme => 0.98f;
    public override float DarkTheme => 0.15f;
}
