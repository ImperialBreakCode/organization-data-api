using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IIndustryRepository : IRepoUpdate<Industry>, IRepoInsert<Industry>
    {
        Industry? GetByIndustryName(string name);
    }
}