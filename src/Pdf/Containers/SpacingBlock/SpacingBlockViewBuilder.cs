namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using SkiaSharp;

/// <summary>
/// Used to add spacing in between blocks.
/// </summary>
public sealed class SpacingBlockViewBuilder : IViewBuilder
{
    private float Height { get; }

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal SpacingBlockViewBuilder(float height)
    {
        Height = height < 700f ? height : 5f;
    }

    public ILayout ToLayout()
    {
        return new SpacingBlockLayout(Height);
    }
}
