using OrganizationData.Data.Abstractions.Repository.RepoCommon;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IIndustryRepository : IRepositoryWithJunction<Industry, IndustryOrganization>
    {
        Industry? GetNonDeletedIndustryByName(string name);
    }
}
