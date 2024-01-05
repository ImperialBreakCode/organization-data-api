using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.Auth;

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

        public User CreateUserEntity(string username)
        {
            return new User()
            {
                Username = username
            };
        }

        public UserRole CreateUserRoleEntity(string name)
        {
            return new UserRole()
            {
                RoleName = name
            };
        }
    }
}
