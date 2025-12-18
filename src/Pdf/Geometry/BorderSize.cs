namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The size of the content plus padding and border for a given style.
/// </summary>
public readonly record struct BorderSize
{
    private SKSize Value { get; }

    internal BorderSize(SKSize size)
    {
        Value = size;
    }

    internal BorderSize(PaddingSize padding, SKSize border)
        : this(padding.ToSize() + border)
    {
    }

    public SKSize ToSize() => Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
