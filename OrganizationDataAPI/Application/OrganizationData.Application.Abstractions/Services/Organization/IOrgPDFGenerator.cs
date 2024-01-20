using OrganizationData.Application.DTO.Organization;

namespace OrganizationData.Application.Abstractions.Services.Organization
{
    public interface IOrgPDFGenerator
    {
        byte[] GetPdf(GetOrganizationResponseDTO organization);
    }
}
