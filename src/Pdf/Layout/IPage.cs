namespace InvoiceKit.Pdf.Layout;

public interface IPage
{
    List<IDrawable> Drawables { get; }
}
