using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Helpers;
using OrganizationData.Data.Repositories.RepoCommon;
using System.Data;

namespace OrganizationData.Data.Repositories
{
    internal class OrganizationRepository : RepositoryWithJunction<Organization, IndustryOrganization>, IOrganizationRepository
    {
        private readonly string _junctionSelectQuery;
        private readonly string _selectByOrgIdQuery;

        public OrganizationRepository(ISqlConnectionWrapper sqlConnectionWrapper) 
            : base(sqlConnectionWrapper)
        {
            _junctionSelectQuery = $"SELECT * FROM {nameof(IndustryOrganization)} WHERE {nameof(IndustryOrganization.OrganizationId)}=@organizationId";
            _selectByOrgIdQuery = $"SELECT * FROM [{nameof(Organization)}] WHERE {nameof(IndustryOrganization.OrganizationId)}=@organizationId";
        }

        public ICollection<IndustryOrganization> GetChildrenFromJunction(string id)
        {
            var command = CreateCommand(_junctionSelectQuery);
            command.Parameters.AddWithValue("@organizationId", id);
            return EntityConverterHelper.ToEntityCollection<IndustryOrganization>(command);
        }

        public Organization? GetByOrganizationId(string organizationId)
        {
            var command = CreateCommand(_selectByOrgIdQuery);
            command.Parameters.AddWithValue("@organizationId", organizationId);

            return EntityConverterHelper.ToEntityCollection<Organization>(command).FirstOrDefault();
        }

        public override void SoftDelete(Organization entity)
        {
            var children = GetChildrenFromJunction(entity.Id);

            foreach (var child in children)
            {
                RemoveJunctionEntity(child);
            }

            base.SoftDelete(entity);
        }

        public bool CheckIfExistsByOrganizationId(string organizationId)
        {
            var command = CreateCommand($"IF EXISTS ({_selectByOrgIdQuery}) SELECT 1 AS OrgExists ELSE SELECT 0 AS OrgExists");
            command.Parameters.AddWithValue("@organizationId", organizationId);
            command.ExecuteNonQuery();

            bool exists = false;
            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                exists = reader.GetInt32("OrgExists") == 1;
            }

            return exists;
        }
    }
}
