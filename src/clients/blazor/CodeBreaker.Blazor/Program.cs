using BlazorApplicationInsights;
using CodeBreaker.Blazor.Authentication;
using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.UI;
using CodeBreaker.UI.Services.Dialog;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.Authentication.RedirectUri = $"{builder.HostEnvironment.BaseAddress}authentication/login-callback";
});
builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<IGameClient, GameClient>();
builder.Services.AddScoped<IGameReportClient, GameClient>();
builder.Services.AddScoped<IDialogService, DialogService>();

var host = builder.Build();

CultureInfo culture;
var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("blazorCulture.get");

if (result != null)
{
    culture = new (result);
}
else
{
    culture = new ("en");
    await js.InvokeVoidAsync("blazorCulture.set", "en");
}

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();