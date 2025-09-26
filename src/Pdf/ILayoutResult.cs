namespace InvoiceKit.Pdf;

public interface ILayoutResult
{
    public IReadOnlyCollection<IDrawable> Drawables { get; }

    public LayoutStatus Status { get; }
}
