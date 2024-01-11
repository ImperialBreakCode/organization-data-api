using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IOrganizationRepository : 
        IRepoInsert<Organization>, IRepoUpdate<Organization>, IRepoJunction<IndustryOrganization>, IRepoDelete<Organization>
    {
        Organization? GetByOrganizationId(string organizationId);
        ICollection<IndustryOrganization> GetChildrenFromJunction(string id);
        bool CheckIfExistsByOrganizationId(string organizationId);
    }
}
