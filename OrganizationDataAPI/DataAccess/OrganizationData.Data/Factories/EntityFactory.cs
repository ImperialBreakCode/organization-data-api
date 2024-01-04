using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Factories
{
    internal class EntityFactory : IEntityFactory
    {
        public Country CreateCountryEntity(string countryName)
        {
            return new Country()
            {
                CountryName = countryName
            };
        }

        public Industry CreateIndustryEntity(string industryName)
        {
            return new Industry()
            {
                IndustryName = industryName
            };
        }

        public IndustryOrganization CreateIndustryOrganizationJunction(string organizationId, string industryId)
        {
            return new IndustryOrganization()
            {
                IndustryId = industryId,
                OrganizationId = organizationId
            };
        }
    }
}
