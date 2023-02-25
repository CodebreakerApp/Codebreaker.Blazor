using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T>
{
    private List<ColumnDefinition<T>> _columnDefinitions = new();
    private string _columnTemplate = string.Empty;

    protected override void OnInitialized()
    {
        foreach (var item in Columns)
        {
            _columnDefinitions.Add(new ColumnDefinition<T>(item.Title, item.FieldSelectorExpression!));
        }
        _columnTemplate = string.Concat(Enumerable.Repeat("1fr ", Columns.Count));
    }

    private async Task OnRowClicked(FluentDataGridRow<T> row)
    {
        if (row.RowData != null)
        {
            await ItemClicked(row.RowData);
        }
    }

    private async Task ItemClicked(T data)
    {
        Console.WriteLine("Item clicked");
        await RowItemClicked.InvokeAsync(data);
    }
}
