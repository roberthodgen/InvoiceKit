namespace InvoiceKit.Pdf;

public interface ILayoutResult
{
    public List<IDrawable> Drawables { get; }

    public LayoutStatus Status { get; }
}
