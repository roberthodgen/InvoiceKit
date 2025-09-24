namespace InvoiceKit.Pdf;

public class LayoutResult : ILayoutResult
{
    public List<IDrawable> Drawables { get; }

    public LayoutState State { get; }

    internal LayoutResult(List<IDrawable> drawables, LayoutState state)
    {
        Drawables = drawables;
        State = state;
    }
}
