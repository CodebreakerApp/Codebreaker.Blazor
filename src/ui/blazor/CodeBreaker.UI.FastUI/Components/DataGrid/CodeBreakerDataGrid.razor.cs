using System.Text.Json;
using CodeBreaker.Shared.Models.Data.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace CodeBreaker.UI
{
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
            Console.WriteLine($"Row clicked. {JsonSerializer.Serialize(row.RowIndex)}");
            if (row.RowIndex != null)
            {
                var rowIndex = row.RowIndex ?? 1;
                var data = Items[rowIndex - 1];
                if (data != null)
                {
                    await RowItemClicked.InvokeAsync(data);
                }
            }
        }
    }
}
