using CodeBreaker.Blazor.Client.Models;
using System.Text;

namespace CodeBreaker.Blazor.Client.Extensions;

internal static class FieldExtensions
{
    public static string GetCssClasses(this Field field)
    {
        var stringBuilder = new StringBuilder();

        if (field.Color is not null)
            stringBuilder.Append($" {field.Color.ToLower()}");

        if (field.Shape is not null)
            stringBuilder.Append($" {field.Shape.ToLower()}");

        if (field.Selected)
            stringBuilder.Append(" selected");

        if (field.CanDrop)
            stringBuilder.Append(" can-drop");

        return stringBuilder.ToString();
    }

    public static string Serialize(this Field field) =>
        string.Join(';', new string?[] { field.Shape, field.Color }.Where(x => x is not null));
}