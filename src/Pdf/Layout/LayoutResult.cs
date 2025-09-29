namespace InvoiceKit.Pdf.Layout;

public sealed class LayoutResult : ILayoutResult
{
    public IReadOnlyCollection<IDrawable> Drawables { get; }

    public LayoutStatus Status { get; }

    internal LayoutResult(List<IDrawable> drawables, LayoutStatus status)
    {
        Drawables = drawables;
        Status = status;
    }
}
