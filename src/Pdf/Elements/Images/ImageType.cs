namespace InvoiceKit.Pdf.Elements.Images;

public readonly record struct ImageType
{
    public static ImageType Svg = new (1);

    public static ImageType Bmp = new (2);

    private int Value { get; }

    private ImageType(int value)
    {
        Value = value;
    }
}
