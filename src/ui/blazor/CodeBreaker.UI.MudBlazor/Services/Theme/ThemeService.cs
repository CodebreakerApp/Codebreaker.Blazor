using CodeBreaker.UI.Shared.Services;
using CodeBreaker.UI.Shared.Services.Theme;
using Microsoft.JSInterop;
using MudBlazor;

namespace CodeBreaker.UI.Services.Theme;
public class ThemeService : ThemeServiceBase<MudTheme>
{
    private MudTheme _defaultTheme = new();

    public ThemeService(IJSRuntime jsRuntime) : base(jsRuntime)
    {
    }

    public override MudTheme DefaultTheme => new()
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
            Black = _defaultTheme.PaletteDark.Black,
            White = _defaultTheme.PaletteDark.White,
            Primary = "#00bbf0",
            PrimaryContrastText = _defaultTheme.PaletteDark.PrimaryContrastText,
            Secondary = _defaultTheme.PaletteDark.Secondary,
            SecondaryContrastText = _defaultTheme.PaletteDark.SecondaryContrastText,
            Tertiary = _defaultTheme.PaletteDark.Tertiary,
            TertiaryContrastText = _defaultTheme.PaletteDark.
            Info = _defaultTheme.PaletteDark.Info,
            InfoContrastText = _defaultTheme.PaletteDark.
            Success = _defaultTheme.PaletteDark.Success,
            SuccessContrastText = _defaultTheme.PaletteDark. SuccessContrastText,
            Warning = _defaultTheme.PaletteDark.Warning,
            WarningContrastText = _defaultTheme.PaletteDark.
            Error = _defaultTheme.PaletteDark.Error,
            ErrorContrastText = _defaultTheme.PaletteDark. ErrorContrastText,
            Dark = _defaultTheme.PaletteDark.Dark,
            DarkContrastText = _defaultTheme.PaletteDark.DarkContrastText,
            TextPrimary = _defaultTheme.PaletteDark.TextPrimary,
            TextSecondary = _defaultTheme.PaletteDark.TextSecondary,
            TextDisabled = _defaultTheme.PaletteDark.TextDisabled,
            ActionDefault = "#00bbf0",
            ActionDisabled = _defaultTheme.PaletteDark.ActionDisabled,
            ActionDisabledBackground = _defaultTheme.PaletteDark.ActionDisabledBackground,
            Background = _defaultTheme.PaletteDark.Background,
            BackgroundGrey = _defaultTheme.PaletteDark.BackgroundGrey,
            Surface = _defaultTheme.PaletteDark.Surface,
            DrawerBackground = _defaultTheme.PaletteDark.DrawerBackground,
            DrawerText = _defaultTheme.PaletteDark.DrawerText,
            DrawerIcon = _defaultTheme.PaletteDark.DrawerIcon,
            AppbarBackground = _defaultTheme.PaletteDark.AppbarBackground,
            AppbarText = "#00bbf0",
            LinesDefault = _defaultTheme.PaletteDark.LinesDefault,
            LinesInputs = _defaultTheme.PaletteDark.LinesInputs,
            TableLines = "#00bbf0",
            TableStriped = _defaultTheme.PaletteDark.TableStriped,
            TableHover = _defaultTheme.PaletteDark.TableHover,
            Divider = _defaultTheme.PaletteDark.Divider,
            DividerLight = _defaultTheme.PaletteDark.DividerLight,
        }
    };
    public override MudTheme DarkTheme => DefaultTheme;
}
