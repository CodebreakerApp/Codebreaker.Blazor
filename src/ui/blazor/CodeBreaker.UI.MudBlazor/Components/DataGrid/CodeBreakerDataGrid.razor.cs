using CodeBreaker.Shared.Models.Data.DataGrid;
using Microsoft.AspNetCore.Components;

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
        public RenderFragment<T> RowContent { get; set; } = default!;
    }
}
