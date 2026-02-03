namespace InvoiceKit.Pdf.Views;

public sealed class ZStack : ContainerBase
{
    internal ZStack(BlockStyle defaultStyle, List<IColumnWidth>? columnWidths)
        : base(defaultStyle,  columnWidths)
    {
    }

    public override ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
