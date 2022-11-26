using CodeBreaker.UI.Shared.Models.DataGrid;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T>
{    
    [Parameter]
    public RenderFragment<T> RowContent { get; set; } = default!;

    private async Task OnRowClicked(TableRowClickEventArgs<T> row)
    {
        if (row.Item != null)
        {
            await RowItemClicked.InvokeAsync(row.Item);
        }
    }
}
