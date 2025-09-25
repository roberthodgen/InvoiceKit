namespace InvoiceKit.Pdf;

public interface IPage
{
    List<IDrawable> Drawables { get; }
}
