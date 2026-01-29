namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Renders content horizontally. Each column is rendered side-by-side.
/// </summary>
/// <remarks>If you need more than one element in a column, use a <see cref="VStack"/> inside of this.</remarks>
public sealed class HStack : ContainerBase, IRow
{
    internal HStack(BlockStyle defaultStyle, List<ColumnWidth>? columnWidths = null)
        : base(defaultStyle,  columnWidths)
    {
    }

    public override ILayout ToLayout()
    {
        var childrenLayouts = Children.Select(child => child.ToLayout()).ToList();
        return new HStackLayout(childrenLayouts);
    }


    public IRow WithColumnWidths(Action<ColumnBuilder> configureColumns)
    {
        var columnWidths = new ColumnBuilder();
        configureColumns(columnWidths);
        ColumnsWidths = columnWidths.Build();
        return this;
    }
}
