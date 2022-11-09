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

        private List<ColumnDefinition<T>> _columnDefinitions = new();

        protected override void OnInitialized()
        {
            foreach(var item in Columns)
            {
                _columnDefinitions.Add(new ColumnDefinition<T>(item.Title, item.FieldSelectorExpression));
            }
        }
    }
}
