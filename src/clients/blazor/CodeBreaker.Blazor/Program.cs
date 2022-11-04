using System.Net.Http;
using System.Runtime.CompilerServices;
using BlazorApplicationInsights;

using CodeBreaker.Blazor;
using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorApplicationInsights();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient("ServerAPI", client =>
                client.BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? throw new InvalidOperationException("Missing ApiBase configuration")));

// TODO: Create Extension method
builder.Services.AddScoped(typeof(IGameClient), services =>
{
    using var scope = services.CreateScope();
    var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ServerAPI");
    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<GameClient>>();

    return new GameClient(httpClient, logger, authService);
});

builder.Services.AddScoped(typeof(IGameReportClient), services =>
{
    using var scope = services.CreateScope();
    var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ServerAPI");
    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<GameClient>>();

    return new GameClient(httpClient, logger, authService);
});

builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddCodeBreakerUI();

await builder.Build().RunAsync();
