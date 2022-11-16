using CodeBreaker.UI.Shared.Services.Dialog;
using CodeBreaker.UI.Services.Dialog;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CodeBreaker.UI;
public static class ServiceCollectionExtensions
{
    public static void AddCodeBreakerUI(this IServiceCollection services)
    {
        services.AddScoped<ICodeBreakerDialogService, MudBlazorDialogService>();
        services.AddMudServices();
    }
}
