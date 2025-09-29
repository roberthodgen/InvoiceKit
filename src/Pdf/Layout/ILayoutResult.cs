namespace InvoiceKit.Pdf.Layout;

internal interface ILayoutResult
{
    public IReadOnlyCollection<IDrawable> Drawables { get; }

    public LayoutStatus Status { get; }
}
