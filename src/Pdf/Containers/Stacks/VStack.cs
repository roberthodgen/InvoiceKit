namespace InvoiceKit.Pdf.Containers.Stacks;

using Styles.Text;

/// <summary>
/// Renders content vertically. Each row is rendered on a new line.
/// </summary>
public class VStack : ContainerBase
{
    internal VStack(TextStyle defaultTextStyle)
        : base(defaultTextStyle)
    {
    }

    public override ILayout ToLayout(PageLayout page)
    {
        throw new NotImplementedException();
    }
}
