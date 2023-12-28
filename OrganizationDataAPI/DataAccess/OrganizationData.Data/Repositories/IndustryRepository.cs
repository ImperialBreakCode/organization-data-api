using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    internal class IndustryRepository : RepositoryWithJunction<Industry, IndustryOrganization>, IIndustryRepository
    {
        public IndustryRepository(ISqlConnectionWrapper sqlConnectionWrapper) : base(sqlConnectionWrapper)
        {
        }

        public Industry? GetNonDeletedIndustryByName(string name)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(Industry)}] WHERE {nameof(Industry.IndustryName)}=@industryName AND {nameof(Industry.DeletedAt)} IS NULL");
            command.Parameters.AddWithValue("@industryName", name);

            return EntityConverterHelper.ToEntityCollection<Industry>(command).FirstOrDefault();
        }

        public override void SoftDelete(Industry entity)
        {
            var command = CreateCommand($"DELETE FROM [{nameof(IndustryOrganization)}] WHERE {nameof(IndustryOrganization.IndustryId)}=@id");
            command.Parameters.AddWithValue("@id", entity.Id);
            command.ExecuteNonQuery();

            base.SoftDelete(entity);
        }
    }
}
