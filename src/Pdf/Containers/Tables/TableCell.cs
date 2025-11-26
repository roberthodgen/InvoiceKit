namespace InvoiceKit.Pdf.Containers.Tables;

using Layouts;
using Views;

/// <summary>
/// Renders a table cell. Currently only supports text via wrapping <see cref="TextLayout"/>.
/// </summary>
public class TableCell : ContainerBase
{
    /// <summary>
    ///
    /// </summary>
    internal TableCell(BlockStyle defaultStyle)
        : base(defaultStyle)
    {
    }

    public override ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
