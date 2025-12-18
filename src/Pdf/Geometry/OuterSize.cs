namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The outermost size after having margin, border, and padding applied to the content size.
/// </summary>
public readonly record struct OuterSize
{
    private SKSize Value { get; }

    public float Width => Value.Width;

    public float Height => Value.Height;

    internal OuterSize(SKSize size)
    {
        Value = size;
    }

    public OuterSize(float width, float height)
        : this(new SKSize(width, height))
    {
    }

    internal OuterSize(BorderSize border, SKSize margin)
    {
        Value = margin + border.ToSize();
    }

    public SKSize ToSize() => Value;

    public BorderSize ToBorderSize(BlockStyle style)
    {
        return new (ToSize() - style.Margin.ToSize());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
