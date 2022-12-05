using CodeBreaker.Blazor.Pages;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.Blazor.Components
{
    public partial class GameResultDialog
    {
        [Parameter]
        public GameMode GameMode { get; set; }
        [Parameter]
        public string Username { get; set; } = string.Empty;
    }
}
