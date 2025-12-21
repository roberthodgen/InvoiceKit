namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The size of the content plus padding for a given style.
/// </summary>
public readonly record struct PaddingSize : ISize
{
    private SKSize Value { get; }

    public float Width => Value.Width;

    public float Height => Value.Height;

    internal PaddingSize(SKSize size)
    {
        Value = size;
    }

    public SKSize ToSize() => Value;

    public ContentSize ToContentSize(BlockStyle style)
    {
        return new (Value - style.Padding.ToSize());
    }

    public PaddingSize ToPaddingSize(BlockStyle style)
    {
        return this;
    }

    public BorderSize ToBorderSize(BlockStyle style)
    {
        return new BorderSize(Value + style.Border.ToSize());
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
