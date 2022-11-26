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
        private readonly Version version = typeof(Program)?.Assembly.GetName().Version;

        protected override void OnInitialized()
        {
            instructions = Loc["About_Instructions"];
            base.OnInitialized();
        }
    }
}