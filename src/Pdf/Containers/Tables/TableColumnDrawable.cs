namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;

public class TableColumnDrawable : IDrawable
{
    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    internal TableColumnDrawable(SKRect rect, bool debug = false)
    {
        SizeAndLocation = rect;
        Debug = debug;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void Draw(IDrawableContext context)
    {
        throw new NotImplementedException();
    }
}
