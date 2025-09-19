namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public sealed class SpacingBlock : IViewBuilder
{
    private float Height { get; }

    internal SpacingBlock(float height)
    {
        Height = height;
    }

    public ILayout ToLayout()
    {
        return new SpacingBlockLayout(Height);
    }
}
