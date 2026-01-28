namespace InvoiceKit.Pdf.Views;

public sealed class ZStack : ContainerBase
{
    internal ZStack(BlockStyle defaultStyle, List<ColumnWidth>? columnWidths)
        : base(defaultStyle,  columnWidths)
    {
    }

    public override ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
