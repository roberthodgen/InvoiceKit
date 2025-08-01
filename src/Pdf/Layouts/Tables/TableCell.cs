namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;
using Text;

/// <summary>
/// Renders a table cell. Currently only supports text via wrapping <see cref="TextBlock"/>.
/// </summary>
public class TableCell : LayoutBuilderBase, IDrawable
{
    public int RowIndex { get; }

    public int ColumnIndex { get; }

    /// <summary>
    /// Table cells should be created by the <see cref="TableColumn"/> to ensure property tracking and assignment.
    /// </summary>
    internal TableCell(TextStyle defaultTextStyle, int rowIndex, int columnIndex)
        : base(defaultTextStyle)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }

    public SKSize Measure(SKSize available)
    {
        return Child?.Measure(available) ?? SKSize.Empty;
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        Child?.Draw(page, rect);
    }
}
