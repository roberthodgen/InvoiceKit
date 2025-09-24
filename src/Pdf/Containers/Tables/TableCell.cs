namespace InvoiceKit.Pdf.Containers.Tables;

using Elements;
using SkiaSharp;
using Styles.Text;

/// <summary>
/// Renders a table cell. Currently only supports text via wrapping <see cref="TextLayout"/>.
/// </summary>
public class TableCell : ElementBase
{
    /// <summary>
    ///
    /// </summary>
    internal TableCell(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }
}
