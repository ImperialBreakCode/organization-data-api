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

        public ICollection<string> GetAllOrganizationIds()
        {
            var command = CreateCommand($"SELECT OrganizationId FROM Organization WHERE DeletedAt IS NULL");
            ICollection<string> ids = new List<string>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ids.Add(reader["OrganizationId"].ToString());
                }
            }

            return ids;
        }
    }
}
