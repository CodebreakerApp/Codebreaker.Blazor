using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CodeBreaker.UI;
public static class ServiceCollectionExtensions
{
    public static void AddCodeBreakerUI(this IServiceCollection services)
    {
        services.AddMudServices();
    }
}
