using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Data
{
    internal class OrganizationDataSeeder : IOrganizationDataSeeder
    {
        public void SeedData(IOrganizationDbContext context)
        {
            Country country = new()
            {
                CountryName = "test country",
            };

            context.Country.Insert(country);

            Organization organization = new()
            {
                CountryId = country.Id,
                Description = "descr",
                Founded = 2000,
                Name = "Infinity-Laguardia",
                NumberOfEmployees = 1000,
                Website = "www.infinity-laguardia.com",
                OrganizationId = "inflaguard-dssfsdf",
            };

            context.Organization.Insert(organization);

            Industry industry = new()
            {
                IndustryName = "aaaa"
            };

            Industry industry2 = new()
            {
                IndustryName = "bbbb"
            };

            context.Industry.Insert(industry);
            context.Industry.Insert(industry2);

            context.Organization.AddJunctionEntity(new IndustryOrganization()
            {
                IndustryId = industry.Id,
                OrganizationId = organization.Id
            });

            context.Industry.AddJunctionEntity(new IndustryOrganization()
            {
                IndustryId = industry2.Id,
                OrganizationId = organization.Id
            });

            context.SaveChanges();
        }
    }
}
