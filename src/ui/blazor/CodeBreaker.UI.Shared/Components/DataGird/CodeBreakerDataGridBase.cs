using CodeBreaker.UI.Shared.Models.DataGrid;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.DataGird;
public abstract class CodeBreakerDataGridBase<T> : ComponentBase
{
    [Parameter]
    public List<string> Headers { get; set; } = new List<string>();

    [Parameter]
    public List<T> Items { get; set; } = new();

    [Parameter]
    public List<CodeBreakerColumnDefinition<T>> Columns { get; set; } = new List<CodeBreakerColumnDefinition<T>>();

    [Parameter]
    public EventCallback<T> RowItemClicked { get; set; } = new();
}
