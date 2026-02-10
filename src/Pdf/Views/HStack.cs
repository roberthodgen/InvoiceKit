namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
/// <remarks>If you need more than one element in a column, use a <see cref="VStack"/> inside of this.</remarks>
public sealed class HStack : ContainerBase, IRow
{
    internal HStack(BlockStyle defaultStyle)
        : base(defaultStyle)
    {
    }

    public override ILayout ToLayout()
    {
        if (ColumnType != ColumnType.Equal && Children.Count != ColumnWidths.Count)
        {
            throw new ApplicationException("Need same number of columns and widths when using custom columns.");
        }

        var columnWidths = ColumnWidths.Count == 0 ? ColumnWidthEqual.CreateEqualColumns(Children.Count) : ColumnWidths;

        var columns = new List<Column>();
        foreach (var index in Enumerable.Range(0, Children.Count))
        {
            columns.Add(new Column(Children[index].ToLayout(), columnWidths[index]));
        }

        return new HStackLayout(columns);
    }

    public IRow WithColumnWidths(Action<ColumnBuilder> configureColumns)
    {
        var builder = new ColumnBuilder();
        configureColumns(builder);
        ColumnType = builder.ColumnType;
        ColumnWidths = builder.ColumnWidths;
        return this;
    }
}
