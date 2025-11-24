namespace InvoiceKit.Pdf.Styles;

using SkiaSharp;

public readonly record struct BorderStyle()
{
    /// <summary>
    /// Width of the boarder
    /// </summary>
    /// <remarks>
    /// A value of <c>0</c> indicates no border.
    /// </remarks>
    public float Width { get; init; } = 0f;

    public SKColor Color { get; init; } = SKColors.Black;

    public bool IsDrawable()
    {
        return Color.Alpha > 0 && Width > 0;
    }

    public SKPaint ToPaint()
    {
        return new SKPaint
        {
            StrokeWidth = Width,
            Color = Color,
        };
    }
}
