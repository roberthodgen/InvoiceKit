namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
/// <remarks>If you need more than one element in a column, use a <see cref="VStack"/> inside of this.</remarks>
public sealed class HStack : ContainerBase, IRow
{
    internal HStack(BlockStyle defaultStyle, List<IColumnWidth>? columnWidths = null)
        : base(defaultStyle,  columnWidths)
    {
    }

    public override ILayout ToLayout()
    {
        if (ColumnWidths is not null && ColumnWidths.Count != Children.Count)
        {
            throw new ApplicationException("Column widths does not equal the number of columns.");
        }

        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new HStackLayout(childrenLayouts, ColumnWidths);
    }


    public IRow WithColumnWidths(Action<ColumnBuilder> configureColumns)
    {
        if (ColumnWidths is not null)
        {
            throw new ApplicationException("Column widths were already set from a parent vStack.");
        }

        var columnWidths = new ColumnBuilder();
        configureColumns(columnWidths);
        ColumnWidths = columnWidths.Build();
        return this;
    }
}
