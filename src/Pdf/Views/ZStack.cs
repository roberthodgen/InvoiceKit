namespace InvoiceKit.Pdf.Views;

public sealed class ZStack : ContainerBase
{
    internal ZStack(BlockStyle defaultStyle)
        : base(defaultStyle)
    {
    }

    public override ILayout ToLayout()
    {
        throw new NotImplementedException();
    }
}
