using BlazorApplicationInsights;
using Codebreaker.GameAPIs.Client;
using CodeBreaker.Blazor.Client.Contracts.Services;
using CodeBreaker.Blazor.Client.Extensions;
using CodeBreaker.Blazor.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluentUIComponents();

builder.Services.AddLocalization();
builder.Services.AddBlazorApplicationInsights();

builder.Services.AddHttpClient("GameApi",  (HttpClient client) =>
    client.BaseAddress = new Uri(builder.Configuration.GetRequired("ApiBase")));

builder.Services.AddHttpClient<IGamesClient, GamesClient>("GameApi");
builder.Services.AddScoped<IMobileDetectorService, MobileDetectorService>();

var host = builder.Build();

await host.ConfigureLocalizationAsync();

await host.RunAsync();