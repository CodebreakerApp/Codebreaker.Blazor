using Microsoft.Extensions.DependencyInjection;
using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI;
public static class ServiceCollectionExtensions
{
    public static void AddCodeBreakerUI(this IServiceCollection services)
    {
        services.AddFluentUIComponents();
    }
}
