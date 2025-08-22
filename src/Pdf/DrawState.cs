namespace InvoiceKit.Pdf;

using SkiaSharp;

public class DrawState
{
    private int CurrentPage { get; set; }

    private SKPoint Cursor { get; set; }

    public DrawState()
    {
        CurrentPage = 1;
        Cursor = new SKPoint(0, 0);
    }

    public DrawState(DrawState from)
    {
        CurrentPage = from.CurrentPage;
        Cursor = from.Cursor;
        // Copy other fields
    }

    public void StartNewPage()
    {
        CurrentPage++;
        Cursor = new SKPoint(0, 0); // Reset as needed
    }

    public void AdjustAfterChild(DrawState child)
    {
        CurrentPage = child.CurrentPage;
    }
}
