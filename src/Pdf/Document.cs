namespace InvoiceKit.Pdf;

using SkiaSharp;

public class Document
{
    // Start a page (size in points: 72 points = 1 inch)
    private const int pageWidth = 612; // US Letter width in points
    private const int pageHeight = 792; // US Letter height in points

    public static void Generate(string path)
    {
        using var stream = File.OpenWrite(path);
        using var doc = SKDocument.CreatePdf(stream);
        using var canvas = doc.BeginPage(pageWidth, pageHeight);

        var layout = new Layout(canvas);

        // Header
        layout.DrawText("Test, Co.", 50, 50);
        layout.DrawText("Invoice #123", 400, 50);
        layout.DrawText("Date: 05/28/2025", 400, 70);

        // Table header
        var colWidths = new float[] { 250, 80, 80, 80, };
        var headers = new[] { "Description", "Quantity", "Unit Price", "Total", };
        layout.DrawTableHeader(120, colWidths, headers);

        // Line items
        string[][] rows =
        [
            ["Website design services", "1", "$2,000.00", "$2,000.00",],
            ["Hosting (12 months)", "1", "$120.00", "$120.00",],
            ["Domain registration", "1", "$15.00", "$15.00",]
        ];

        const float rowStartY = 145;
        for (var i = 0; i < rows.Length; i++)
        {
            layout.DrawRow(rowStartY + i * 25, colWidths, rows[i]);
        }

        // Total
        layout.DrawText("Total:", 385, rowStartY + rows.Length * 30);
        layout.DrawText("$2,135.00", 465, rowStartY + rows.Length * 30);

        doc.EndPage();
        doc.Close();

        Console.WriteLine($"PDF created: {path}");
    }
}
