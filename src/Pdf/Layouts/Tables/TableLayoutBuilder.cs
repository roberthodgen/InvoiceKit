namespace InvoiceKit.Pdf.Layouts.Tables;

using SkiaSharp;
using Styles.Text;

public class TableLayoutBuilder(TextStyle defaultTextStyle) : IDrawable
{
    private readonly List<TableRow> _headers = [];

    private TextStyle _defaultTextStyle = defaultTextStyle; // TODO make this editable for the table

    public List<TableRow> Rows { get; } = [];

    public List<TableColumn> Columns = [];

    /// <summary>
    /// Specifies how column sizes will be computed.
    /// </summary>
    public ColumnSizing ColumnSizing { get; private set; } = ColumnSizing.Equal;

    public List<ColumnWidthPercent> ColumnWidthPercentages { get; private set; } = [];

    public TextStyle TableHeaderStyle { get; } = defaultTextStyle with { FontPath = "Open Sans/Bold", };

    public bool ShowRowSeparators { get; set; } = false;

    public TableLayoutBuilder AddHeader(Action<TableRow> config)
    {
        var row = new TableRow(this, TableHeaderStyle);
        config(row);
        _headers.Add(row);
        return this;
    }

    public TableLayoutBuilder ConfigureText(Action<TextOptionsBuilder> options)
    {
        var builder = new TextOptionsBuilder(_defaultTextStyle);
        options(builder);
        _defaultTextStyle = builder.Build();
        return this;
    }

    public TableLayoutBuilder AddRow(Action<TableRow> config)
    {
        var row = new TableRow(this, _defaultTextStyle);
        config(row);
        Rows.Add(row);
        return this;
    }

    public SKSize Measure(SKSize available)
    {
        var width = available.Width; // always fills the available width
        var height = Rows.Sum(row => row.Measure(available).Height);
        height += _headers.Sum(row => row.Measure(available).Height);
        return new SKSize(width, height);
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        var top = rect.Top;
        foreach (var row in _headers)
        {
            var rowHeight = row.Measure(rect.Size).Height;
            row.Draw(page, new SKRect(rect.Left, top, rect.Left + rect.Width, top + rowHeight));
            top += rowHeight;
        }

        foreach (var row in Rows)
        {
            // TODO detect when page changes and re-draw header row(s)
            var rowHeight = row.Measure(rect.Size).Height;
            row.Draw(page, new SKRect(rect.Left, top, rect.Left + rect.Width, top + rowHeight));
            top += rowHeight;
        }
    }

    public TableLayoutBuilder UseEquallySpaceColumns()
    {
        ColumnSizing = ColumnSizing.Equal;
        return this;
    }

    public TableLayoutBuilder UseFixedColumnWidths(List<ColumnWidthPercent> columnWidths)
    {
        ColumnSizing = ColumnSizing.FixedPercentage;
        ColumnWidthPercentages = columnWidths;
        return this;
    }

    /// <summary>
    /// Adds row separators between rows. 
    /// </summary>
    public TableLayoutBuilder AddRowSeparators()
    {
        ShowRowSeparators = true;
        return this;
    }

    internal TableColumn GetOrAddColumn(int columnIndex)
    {
        var column = Columns.ElementAtOrDefault(columnIndex);
        if (column is null)
        {
            // TODO handle sizing
            column = new TableColumn(columnIndex);
            Columns.Add(column);
        }

        return column;
    }

    public float GetColumnWidth(float available, int columnIndex) =>
        ColumnSizing switch
        {
            ColumnSizing.Equal => available / Columns.Count,
            ColumnSizing.FixedPercentage => available * ColumnWidthPercentages[columnIndex].Percent,
            ColumnSizing.FixedPoints => throw new NotImplementedException("TODO"),
            ColumnSizing.Auto => throw new NotImplementedException("TODO"),
            _ => throw new NotImplementedException(),
        };

    public void Dispose()
    {
    }
}
