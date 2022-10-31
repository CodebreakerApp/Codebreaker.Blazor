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
builder.Services.AddScoped<IGameReportClient, GameClient>();
builder.Services.AddScoped<IGameClient, GameClient>();
builder.Services.AddCodeBreakerUI();
builder.Services.AddScoped(sp => new HttpClient {
    BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? throw new InvalidOperationException("Missing ApiBase configuration"))
});

await builder.Build().RunAsync();
