using System.Text.Json;
using CodeBreaker.UI.Shared.Models.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T>
{
    [Parameter]
    public List<string> Headers { get; set; } = new List<string>();

    [Parameter]
    public List<T> Items { get; set; } = new();

    [Parameter]
    public List<CodeBreakerColumnDefinition<T>> Columns { get; set; } = new List<CodeBreakerColumnDefinition<T>>();

    [Parameter]
    public EventCallback<T> RowItemClicked { get; set; } = new();

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
        if (row.RowIndex.HasValue)
        {
            try
            {
                var rowIndex = row.RowIndex.Value > 0 ? row.RowIndex.Value : 1;
                var data = Items[rowIndex - 1];
                if (data != null)
                {
                    await RowItemClicked.InvokeAsync(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Row {row.RowIndex}, ItemsCount: {Items.Count}, Error: {ex.Message}");
            }
        }
    }
}
