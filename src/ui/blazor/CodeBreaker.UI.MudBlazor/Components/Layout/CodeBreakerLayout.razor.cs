using MudBlazor;

namespace CodeBreaker.UI;

public partial class CodeBreakerLayout
{
    private MudTheme _defaultTheme = new()
    {
        Palette = new Palette
        {
            AppbarBackground = "#8eb8e5",
            AppbarText = "#fff",
            Primary = "#6b7f82",
            PrimaryContrastText = "#fff",
            Secondary = "#5b5750",
            SecondaryContrastText = "#fff",
            Background = "#fff",
            TextPrimary = "#000",
        },
        PaletteDark = new Palette
        {
            AppbarBackground = "#284F46",
            AppbarText = "#fff",
            Surface = "#3F4F4C",
            DrawerBackground = "#A5CFC6",
            DrawerText = "#fff",
            DrawerIcon = "#fff",
            Primary = "#6b7f82",
            PrimaryContrastText = "#fff",
            Secondary = "#5b5750",
            SecondaryContrastText = "#fff",
            Background = "#1C2321",
            TextPrimary = "#fff",
            ActionDefault = "#fff",
            TextSecondary = "#fff",
        }
    };
}
