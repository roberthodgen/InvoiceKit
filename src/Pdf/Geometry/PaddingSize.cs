namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// The size of the content plus padding for a given style.
/// </summary>
public readonly record struct PaddingSize
{
    private SKSize Value { get; }

    internal PaddingSize(SKSize size)
    {
        Value = size;
    }

    internal PaddingSize(ContentSize contentSize, SKSize padding)
        : this(contentSize.ToSize() + padding)
    {
    }

    public SKSize ToSize() => Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
