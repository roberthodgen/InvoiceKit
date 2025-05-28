namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;

public class DocumentTests
{
    [Fact]
    public void Document_Generate_OutputsPdf()
    {
        const string fileName = "test.pdf";
        File.Delete(fileName);
        Document.Generate(fileName);
        File.Exists(fileName).ShouldBeTrue();
    }
}
