namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The outermost size after having margin, border, and padding applied to the content size.
/// </summary>
public readonly record struct OuterSize : ISize
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

    public ContentSize ToContentSize(BlockStyle style)
    {
        return ToPaddingSize(style).ToContentSize(style);
    }

    public PaddingSize ToPaddingSize(BlockStyle style)
    {
        return ToBorderSize(style).ToPaddingSize(style);
    }

    public BorderSize ToBorderSize(BlockStyle style)
    {
        return new BorderSize(Value - style.Margin.ToSize());
    }

    public OuterSize ToOuterSize(BlockStyle style)
    {
        return this;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
