using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CodeBreaker.UI.Shared.Models.Icon;
using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI.Utils;
public static class IconHelper
{
    public static string GetIcon(CodeBreakerIcon icon)
    {
        return icon switch
        {
            CodeBreakerIcon.Global => FluentIcons.Globe,
            CodeBreakerIcon.Play => FluentIcons.Play,
            CodeBreakerIcon.Cancel => FluentIcons.ShareScreenStop,
            CodeBreakerIcon.Login => FluentIcons.Home,
            CodeBreakerIcon.Logout => FluentIcons.HomePerson,
            CodeBreakerIcon.Info => FluentIcons.Info,
            CodeBreakerIcon.Stats => FluentIcons.GanttChart,
            _ => string.Empty
        };
    }
}
