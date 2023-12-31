using System.Globalization;
using BlazorApplicationInsights;
using CodeBreaker.Blazor;
using CodeBreaker.Blazor.Authentication;
using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization();
builder.Services.AddBlazorApplicationInsights();
builder.Services.AddCodeBreakerUI();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Authentication
builder.Services.AddHttpClient("ServerAPI", client =>
                client.BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? throw new InvalidOperationException("Missing ApiBase configuration")));

//builder.Services.AddHttpClient("ServerAPI", client =>
//                client.BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? throw new InvalidOperationException("Missing ApiBase configuration")));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.Authentication.RedirectUri = $"{builder.HostEnvironment.BaseAddress}authentication/login-callback";
});
builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped(typeof(IGameClient), CreateGameClient);
builder.Services.AddScoped(typeof(IGameReportClient), CreateGameClient);

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    culture = new CultureInfo("en");
    await js.InvokeVoidAsync("blazorCulture.set", "en");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();


static GameClient CreateGameClient(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("ServerAPI");
    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<GameClient>>();

    return new GameClient(httpClient, logger, authService);
}
