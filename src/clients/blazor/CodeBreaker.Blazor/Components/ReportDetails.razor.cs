using Microsoft.AspNetCore.Components;
using CodeBreaker.Shared.Models.Api;

namespace CodeBreaker.Blazor.Components;

public partial class ReportDetails
{
    [Parameter]
    public GameDto? Game { get; set; }
}
