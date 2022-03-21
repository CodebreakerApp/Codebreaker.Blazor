using BlazorMM.Server.Data;
using BlazorMM.Server.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IMastermindContext, MastermindContext>(options =>
{
    string connectionString = builder.Configuration.GetSection("MMServer").GetConnectionString("MastermindConnection");
    options.UseCosmos(connectionString, "Mastermind");
});
builder.Services.AddTransient<IGameInitializer, RandomGameGenerator>();
builder.Services.AddSingleton<GameManager>();
builder.Services.AddTransient<GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
