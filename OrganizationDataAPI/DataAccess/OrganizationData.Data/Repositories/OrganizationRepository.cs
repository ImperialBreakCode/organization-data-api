using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    internal class OrganizationRepository : RepositoryWithJunction<Organization, IndustryOrganization>, IOrganizationRepository
    {
        private readonly string _junctionSelectQuery;

        public OrganizationRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction) 
            : base(sqlConnection, sqlTransaction)
        {
            _junctionSelectQuery = $"SELECT * FROM {nameof(IndustryOrganization)} WHERE OrganizatioId=@organizationId";
        }

        public ICollection<IndustryOrganization> GetChildrenFromJunction(string id)
        {
            var command = CreateCommand(_junctionSelectQuery);
            command.Parameters.AddWithValue("@organizationId", id);
            return EntityConverterHelper.ToEntityCollection<IndustryOrganization>(command);
        }

        public Organization? GetByOrganizationId(string organizationId)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(Organization)}] WHERE OrganizationId=@organizationId");
            command.Parameters.AddWithValue("@organizationId", organizationId);

            return EntityConverterHelper.ToEntityCollection<Organization>(command).FirstOrDefault();
        }
    }
}
