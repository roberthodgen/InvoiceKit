namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;
using Text;

/// <summary>
/// Renders a table cell. Currently only supports text via wrapping <see cref="TextBlock"/>.
/// </summary>
public class TableCell : IDrawable
{
    public TextStyle Style { get; private set; }

    public int RowIndex { get; }

    public int ColumnIndex { get; }

    /// <summary>
    /// Table cells should be created by the <see cref="TableColumn"/> to ensure property tracking and assignment.
    /// </summary>
    internal TableCell(TextStyle style, int rowIndex, int columnIndex)
    {
        Style = style;
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
    }

    public string Text { get; private set; } = "";

    public TableCell AddText(string text)
    {
        return AddText(text, _ => { });
    }

    public TableCell AddText(string text, Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(Style);
        options(builder);
        Style = builder.Build();
        Text = text;
        return this;
    }

    public SKSize Measure(SKSize available)
    {
        return new TextBlock(Style).AddLine(Text).Measure(available);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        new TextBlock(Style).AddLine(Text).Draw(page, rect);
    }
}
