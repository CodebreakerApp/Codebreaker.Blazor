using BlazorApplicationInsights;

using CodeBreaker.Blazor;
using CodeBreaker.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazorApplicationInsights();
builder.Services.AddTransient<IGameReportClient, GameClient>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["CodeBreakerServer"] ?? "localhost:9400") });

await builder.Build().RunAsync();
