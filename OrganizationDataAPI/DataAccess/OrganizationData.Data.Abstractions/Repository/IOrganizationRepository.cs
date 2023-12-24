using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IOrganizationRepository : IRepoInsert<Organization>, IRepoUpdate<Organization>, IRepoJunction<IndustryOrganization>
    {
        Organization? GetByOrganizationId(string organizationId);
    }
}
