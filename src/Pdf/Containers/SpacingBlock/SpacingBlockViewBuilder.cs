namespace InvoiceKit.Pdf.Containers.SpacingBlock;

using Layout;

/// <summary>
/// Used to add vertical spacing between elements.
/// </summary>
public sealed class SpacingBlockViewBuilder(float height) : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new SpacingBlockLayout(height);
    }
}
