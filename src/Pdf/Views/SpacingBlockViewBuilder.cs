namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Adds vertical spacing between elements.
/// </summary>
public sealed class SpacingBlockViewBuilder(float height) : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new SpacingBlockLayout(height);
    }
}
