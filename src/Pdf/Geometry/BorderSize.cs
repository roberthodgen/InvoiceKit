namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The size of the content plus padding and border for a given style.
/// </summary>
public readonly record struct BorderSize : ISize
{
    private SKSize Value { get; }

    public float Width => Value.Width;

    public float Height => Value.Height;

    internal BorderSize(SKSize size)
    {
        Value = size;
    }

    internal BorderSize(PaddingSize padding, SKSize border)
        : this(padding.ToSize() + border)
    {
    }

    public SKSize ToSize() => Value;

    public ContentSize ToContentSize(BlockStyle style)
    {
        return ToPaddingSize(style).ToContentSize(style);
    }

    public PaddingSize ToPaddingSize(BlockStyle style)
    {
        return new PaddingSize(Value - style.Border.ToSize());
    }

    public BorderSize ToBorderSize(BlockStyle style)
    {
        return this;
    }

    public OuterSize ToOuterSize(BlockStyle style)
    {
        return new OuterSize(Value + style.Margin.ToSize());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
