using CodeBreaker.Blazor.Client.Contracts.Services;
using Microsoft.JSInterop;

namespace CodeBreaker.Blazor.Client.Services;

public class MobileDetectorService(IJSRuntime js) : IMobileDetectorService
{
    private static bool? s_isMobile;

    public async ValueTask<bool> IsMobileAsync()
    {
        if (s_isMobile.HasValue)
            return s_isMobile.Value;

        s_isMobile = await js.InvokeAsync<bool>("isMobile");
        return s_isMobile.Value;
    }
}
