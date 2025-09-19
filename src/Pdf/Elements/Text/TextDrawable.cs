namespace InvoiceKit.Pdf.Elements.Text;

using Containers;
using SkiaSharp;
using Styles.Text;

public sealed class TextDrawable : IDrawable
{
    private string TextLine { get; }

    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    private TextStyle Style { get; }

    public TextDrawable(string text, SKRect sizeAndLocation, TextStyle style, bool debug)
    {
        TextLine = text;
        SizeAndLocation = sizeAndLocation;
        Style = style;
        Debug = debug;
    }

    public void Draw(SKCanvas canvas, Page page)
    {
        if (Debug)
        {
            canvas.DrawLine(SizeAndLocation.Left, SizeAndLocation.Top, SizeAndLocation.Right, SizeAndLocation.Top,
                new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 1f,
            });
        }
        canvas.DrawText(TextLine, SizeAndLocation.Left, SizeAndLocation.Top, SKTextAlign.Left, Style.ToFont(), Style.ToPaint());
    }

    public void Dispose()
    {
    }
}
