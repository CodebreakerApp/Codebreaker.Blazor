using BlazorApplicationInsights;
using CodeBreaker.Blazor.Authentication;
using CodeBreaker.Blazor.Extensions;
using CodeBreaker.Services;
using CodeBreaker.Services.Authentication;
using CodeBreaker.UI;
using CodeBreaker.UI.Services.Dialog;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization();
builder.Services.AddBlazorApplicationInsights();
builder.Services.AddCodeBreakerUI();

builder.Services.AddHttpClient("GameApi",  (HttpClient client) =>
    client.BaseAddress = new Uri(builder.Configuration.GetRequired("ApiBase")));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.Authentication.RedirectUri = $"{builder.HostEnvironment.BaseAddress}authentication/login-callback";
});
builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddHttpClient<IGameClient, GameClient>("GameApi");
builder.Services.AddHttpClient<IGameReportClient, GameClient>("GameApi");
builder.Services.AddScoped<IDialogService, DialogService>();

var host = builder.Build();

await host.ConfigureLocalizationAsync();

await host.RunAsync();