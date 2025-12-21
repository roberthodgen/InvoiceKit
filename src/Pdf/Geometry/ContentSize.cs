namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// Represents the size of a content area.
/// </summary>
/// <remarks>
/// The content area is inset within the padding, border, and margin.
/// </remarks>
public readonly record struct ContentSize : ISize
{
    public static ContentSize Empty => new (SKSize.Empty);

    private SKSize Value { get; }

    public float Width => Value.Width;

    public float Height => Value.Height;

    public ContentSize(float width, float height)
        : this(new SKSize(width, height))
    {
    }

    public ContentSize(SKSize value)
    {
        Value = value;
    }

    public SKSize ToSize() => Value;

    public ContentSize ToContentSize(BlockStyle style)
    {
        return this;
    }

    public PaddingSize ToPaddingSize(BlockStyle style)
    {
        return new PaddingSize(Value + style.Padding.ToSize());
    }

    public BorderSize ToBorderSize(BlockStyle style)
    {
        return ToPaddingSize(style).ToBorderSize(style);
    }

    public OuterSize ToOuterSize(BlockStyle style)
    {
        return ToBorderSize(style).ToOuterSize(style);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
