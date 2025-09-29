namespace InvoiceKit.Pdf.Elements.Text;

using SkiaSharp;
using Styles.Text;

internal sealed class TextDrawable : IDrawable
{
    private string TextLine { get; }

    public SKRect SizeAndLocation { get; }

    private TextStyle Style { get; }

    public TextDrawable(string text, SKRect sizeAndLocation, TextStyle style)
    {
        TextLine = text;
        SizeAndLocation = sizeAndLocation;
        Style = style;
    }

    public void Draw(IDrawableContext context)
    {
        if (context.Debug)
        {
            context.Canvas.DrawLine(SizeAndLocation.Left, SizeAndLocation.Top, SizeAndLocation.Right, SizeAndLocation.Top,
                new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 1f,
            });
        }
        context.Canvas.DrawText(TextLine, SizeAndLocation.Left, SizeAndLocation.Top, SKTextAlign.Left, Style.ToFont(), Style.ToPaint());
    }

    public void Dispose()
    {
    }
}
