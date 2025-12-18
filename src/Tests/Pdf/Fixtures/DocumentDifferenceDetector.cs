namespace InvoiceKit.Tests.Pdf.Fixtures;

using System.Runtime.Versioning;
using PDFtoImage;

public class DocumentDifferenceDetector
{
    private byte[] _generatedDocument;

    private string _fileName;

    [SupportedOSPlatform( "windows")]
    [SupportedOSPlatform( "linux")]
    [SupportedOSPlatform( "macos")]
    public DocumentDifferenceDetector(byte[] generatedDocument, string fileName)
    {
        _generatedDocument = generatedDocument;
        _fileName = fileName;

        // 1. Read the archived reference document
        // 2. Compare the two documents on:
        //    - Page Count
        //    - Page Size
        //    - Content of Each Page via visual comparison
        // 3. Logs any failures above
        // 4. Saves the generated document for debugging with a unit test output link

        var referenceDocument = File.ReadAllBytes(_fileName);
        var generatedPages = Conversion.GetPageCount(_generatedDocument);
        foreach (var i in Enumerable.Range(0, generatedPages))
        {
            var pageSize = Conversion.GetPageSize(_generatedDocument, i);
            var generatedPageImage = Conversion.ToImage(_generatedDocument, i);
            var referencePageImage = Conversion.ToImage(referenceDocument, i);
        }
    }
}
