namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;

public class TableRowDrawable : IDrawable
{
    public SKRect SizeAndLocation { get; }
    
    private TableRowViewBuilder TableRowViewBuilder { get; }

    internal TableRowDrawable(SKRect rect, TableRowViewBuilder tableRowViewBuilder)
    {
        SizeAndLocation = rect;
        TableRowViewBuilder = tableRowViewBuilder;
    }

    public void Draw(IDrawableContext context)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
