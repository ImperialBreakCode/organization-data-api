using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.Auth;

namespace OrganizationData.Data.Abstractions.Factories
{
    public interface IEntityFactory
    {
        Industry CreateIndustryEntity(string industryName);
        IndustryOrganization CreateIndustryOrganizationJunction(string organizationId, string industryId);
        Country CreateCountryEntity(string countryName);
        User CreateUserEntity(string username);
        UserRole CreateUserRoleEntity(string name);
    }
}
