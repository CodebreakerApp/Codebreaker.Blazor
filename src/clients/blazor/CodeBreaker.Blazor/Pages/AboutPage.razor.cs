using CodeBreaker.Blazor.Resources;
using CodeBreaker.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace CodeBreaker.Blazor.Pages
{
    public partial class AboutPage
    {
        [Inject]
        private IStringLocalizer<Resource> Loc { get; set; } = default!;

        private string instructions = string.Empty;
        private readonly Version version = typeof(Program)?.Assembly.GetName().Version;

        protected override async Task OnInitializedAsync()
        {
            instructions = Loc["About_Instructions"];
            await base.OnInitializedAsync();
        }
    }
}
