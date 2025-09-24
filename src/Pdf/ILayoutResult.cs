namespace InvoiceKit.Pdf;

public interface ILayoutResult
{
    public List<IDrawable> Drawables { get; }

    public LayoutState State { get; }
}
