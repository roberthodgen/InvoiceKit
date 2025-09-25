namespace InvoiceKit.Pdf;

public class LayoutResult : ILayoutResult
{
    public List<IDrawable> Drawables { get; }

    public LayoutStatus Status { get; }

    internal LayoutResult(List<IDrawable> drawables, LayoutStatus status)
    {
        Drawables = drawables;
        Status = status;
    }
}
