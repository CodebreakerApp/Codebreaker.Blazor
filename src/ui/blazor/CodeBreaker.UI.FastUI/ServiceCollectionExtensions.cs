using CodeBreaker.UI.Services.Cache;
using CodeBreaker.UI.Services.Theme;
using CodeBreaker.UI.Shared.Services;
using CodeBreaker.UI.Shared.Services.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Fast.Components.FluentUI.Infrastructure;

namespace CodeBreaker.UI;
public static class ServiceCollectionExtensions
{
    public static void AddCodeBreakerUI(this IServiceCollection services)
    {
        services.AddScoped<ICodeBreakerDialogService, CodeBreakerDialogService>();
        services.AddScoped<IThemeService<float>, ThemeService>();
        LibraryConfiguration config = new(ConfigurationGenerator.GetIconConfiguration(), ConfigurationGenerator.GetEmojiConfiguration());
        services.AddFluentUIComponents(config);
        services.AddScoped<CodeBreakerCacheStorageAccessor>();
        services.AddScoped<IStaticAssetService, CodeBreakerHttpBasedStaticAssetService>();
    }
}
