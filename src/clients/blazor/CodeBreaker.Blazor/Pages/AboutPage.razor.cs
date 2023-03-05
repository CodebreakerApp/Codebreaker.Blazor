using System.Reflection;
using CodeBreaker.Blazor.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace CodeBreaker.Blazor.Pages
{
    public partial class AboutPage
    {
        [Inject]
        private IStringLocalizer<Resource> Loc { get; set; } = default!;

        private string instructions = string.Empty;
        private string version = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            instructions = Loc["About_Instructions"];
            var currentVersion = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            version = String.IsNullOrWhiteSpace(currentVersion) ? Loc["About_NoVersion_Found"] : currentVersion;
            await base.OnInitializedAsync();
        }
    }
}
