using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.QueryStatsResults;
using OrganizationData.Data.Helpers;
using OrganizationData.Data.Repositories.RepoCommon;

namespace OrganizationData.Data.Repositories
{
    internal class StatsRepository : BaseRepository, IStatsRepository
    {
        public StatsRepository(ISqlConnectionWrapper sqlConnectionWrapper) : base(sqlConnectionWrapper)
        {
        }

        public ICollection<OrganizationCountByCountry> GetOrganizationCountByCountries()
        {
            var command = CreateCommand("SELECT c.CountryName, Count(org.OrganizationId) AS OrganizationCount FROM Organization AS org INNER JOIN Country AS c ON org.CountryId=c.Id AND org.DeletedAt IS NULL AND c.DeletedAt IS NULL GROUP BY c.CountryName");
            
            return EntityConverterHelper.ToEntityCollection<OrganizationCountByCountry>(command);
        }

        public ICollection<OrganizationCountByIndustry> GetOrganizationCountByIndustries()
        {
            var command = CreateCommand("SELECT i.IndustryName, COUNT(iorg.OrganizationId) AS OrganizationCount FROM Industry AS i INNER JOIN IndustryOrganization AS iorg ON i.Id=iorg.IndustryId AND i.DeletedAt IS NULL GROUP BY i.IndustryName");

            return EntityConverterHelper.ToEntityCollection<OrganizationCountByIndustry>(command);
        }

        public ICollection<Organization> GetTopTenOrganizationsWithMostWorkers()
        {
            var command = CreateCommand("SELECT TOP 10 * FROM Organization ORDER BY NumberOfEmployees DESC");

            return EntityConverterHelper.ToEntityCollection<Organization>(command);
        }

        public ICollection<Organization> GetTopTenYoungestOrganizations()
        {
            var command = CreateCommand("SELECT TOP 10 * FROM Organization ORDER BY Founded DESC");

            return EntityConverterHelper.ToEntityCollection<Organization>(command);
        }
    }
}
