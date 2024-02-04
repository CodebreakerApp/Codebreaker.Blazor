using CodeBreaker.UI.Services.Theme;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBreaker.UI;
public static class ServiceCollectionExtensions
{
    public static void AddCodeBreakerUI(this IServiceCollection services)
    {
        //services.AddScoped<ICodeBreakerDialogService, CodeBreakerDialogService>();
        //services.AddScoped<IThemeService<float>, ThemeService>();
    }
}
