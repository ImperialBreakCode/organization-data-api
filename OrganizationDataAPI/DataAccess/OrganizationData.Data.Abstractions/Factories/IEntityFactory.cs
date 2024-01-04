using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Factories
{
    public interface IEntityFactory
    {
        Industry CreateIndustryEntity(string industryName);
        IndustryOrganization CreateIndustryOrganizationJunction(string organizationId, string industryId);
        Country CreateCountryEntity(string countryName);
    }
}
