using OrganizationData.Application.Abstractions.DailyStats;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OrganizationData.Application.DailyStats
{
    internal class PDFGenerator : IPDFGenerator
    {
        public byte[] GeneratePdfFromStats(Dictionary<DateTime, CsvStatsData> datas)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container => 
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header()
                    .Height(100)
                    .Element(container => ComposeHeader(container, datas.First().Key, datas.Last().Key));

                    page.Content().Element(container => ComposeContent(container, datas));
                });

            }).GeneratePdf();
        }

        private void ComposeHeader(IContainer container, DateTime startingDate, DateTime endDate)
        {
            container.Row(row =>
            {
                row.RelativeItem().AlignMiddle().Column(column =>
                {
                    column
                    .Item()
                    .Text("Daily Stats")
                    .SemiBold().FontSize(36)
                    .FontColor(Colors.Blue.Medium);
                });

                row.RelativeItem().AlignMiddle().Column(column =>
                {
                    column
                    .Item()
                    .Text($"Stats range: {startingDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}")
                    .SemiBold().FontSize(11)
                    .FontColor(Colors.Black);
                });
            });
        }

        private void ComposeContent(IContainer container, Dictionary<DateTime, CsvStatsData> datas)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    IContainer HeaderCellStyle(IContainer container) => CellStyle(container, Colors.Blue.Lighten5).ShowOnce();

                    header.Cell().Element(HeaderCellStyle).Text("Date");
                    header.Cell().Element(HeaderCellStyle).Text("Processed csv rows");
                    header.Cell().Element(HeaderCellStyle).Text("Number of countries saved");
                    header.Cell().Element(HeaderCellStyle).Text("Number of industries saved");
                    header.Cell().Element(HeaderCellStyle).Text("Number of organizations saved");
                });

                IContainer NormalCellStyle(IContainer container) => CellStyle(container, Colors.White);
                var textStyle = TextStyle.Default;

                foreach (var item in datas)
                {
                    table.Cell().Element(NormalCellStyle).Text(item.Key.ToString("dd.MM.yyyy"), textStyle);
                    table.Cell().Element(NormalCellStyle).Text(item.Value.ReadRowsCount, textStyle);
                    table.Cell().Element(NormalCellStyle).Text(item.Value.SavedCountriesCount, textStyle);
                    table.Cell().Element(NormalCellStyle).Text(item.Value.SavedIndustriesCount, textStyle);
                    table.Cell().Element(NormalCellStyle).Text(item.Value.SavedOrgsCount, textStyle);
                }
            });
        }

        private IContainer CellStyle(IContainer container, string backgroundColor)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Grey.Darken4)
                .Background(backgroundColor)
                .PaddingVertical(5)
                .PaddingHorizontal(10)
                .AlignCenter()
                .AlignMiddle();
        }

    }
}
