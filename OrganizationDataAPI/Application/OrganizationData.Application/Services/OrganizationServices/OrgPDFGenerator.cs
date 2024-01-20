using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.DTO.Organization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace OrganizationData.Application.Services.OrganizationServices
{
    internal class OrgPDFGenerator : IOrgPDFGenerator
    {
        public byte[] GetPdf(GetOrganizationResponseDTO organization)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(15));

                    page.Header().Height(100).Element(cont => ComposeHeader(cont, organization.OrganizationId));

                    page.Content().Element(container => ComposeContent(container, organization));
                });

            }).GeneratePdf();
        }

        private void ComposeHeader(IContainer container, string orgId)
        {
            container.Row(row =>
            {
                row.RelativeItem().AlignMiddle().Column(column =>
                {
                    column
                    .Item()
                    .Text("Organization Info")
                    .SemiBold().FontSize(30)
                    .FontColor(Colors.Blue.Medium);
                });

                row.RelativeItem().AlignMiddle().Column(column =>
                {
                    column
                    .Item()
                    .Text($"Org ID: {orgId}")
                    .SemiBold().FontSize(11)
                    .FontColor(Colors.Black);
                });
            });
        }

        private void ComposeContent(IContainer container, GetOrganizationResponseDTO organization)
        {
            container.Column(column =>
            {
                column.Item().Text($"Name: {organization.Name}");

                column.Item().Text($"Website: {organization.Website}");

                column.Item().Text($"Country: {organization.Country}");

                column.Item().Text($"Founded: {organization.Founded}");

                column.Item().Text($"Number of employees: {organization.NumberOfEmployees}");

                column.Item().Text($"Industries: {string.Join(", ", organization.Industries)}");

                column.Item().Height(10);

                column.Item().Text("Description: ").SemiBold();
                column.Item().Text(organization.Description);
            });
        }
    }
}
