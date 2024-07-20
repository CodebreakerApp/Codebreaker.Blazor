using BlazorApplicationInsights;
using Codebreaker.GameAPIs.Client;
using CodeBreaker.Blazor.Client.Configuration;
using CodeBreaker.Blazor.Client.Contracts.Services;
using CodeBreaker.Blazor.Client.Extensions;
using CodeBreaker.Blazor.Client.Services;
using CodeBreaker.Blazor.Client.Services.HttpMessageHandlers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddRemoteServiceDiscovery(new Uri (builder.HostEnvironment.BaseAddress));

builder.Services.AddFluentUIComponents();

builder.Services.AddLocalization();
builder.Services.AddBlazorApplicationInsights();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("profile");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://codebreaker3000.onmicrosoft.com/gateway/games.play");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://codebreaker3000.onmicrosoft.com/gateway/live.observe");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://codebreaker3000.onmicrosoft.com/gateway/ranking.query");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("https://codebreaker3000.onmicrosoft.com/gateway/bot.trigger");
    options.UserOptions.NameClaim = "extension_GamerName";
});

builder.Services.AddTransient<AllAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IGamerNameSuggestionClient, GamerNameSuggestionClient>(configure =>
    configure.BaseAddress = new Uri("https://gateway/users/public")
)
.ConfigureRemoteServiceDiscovery();

builder.Services.AddHttpClient<IGamesClient, GamesClient>(configure =>
    configure.BaseAddress = new Uri("https://gateway/games/")
)
.ConfigureRemoteServiceDiscovery()
.AddHttpMessageHandler<AllAuthorizationMessageHandler>();

builder.Services.AddScoped<IMobileDetectorService, MobileDetectorService>();

var host = builder.Build();

await host.ConfigureLocalizationAsync();

await host.RunAsync();