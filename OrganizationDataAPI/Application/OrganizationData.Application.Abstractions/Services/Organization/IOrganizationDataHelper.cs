using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Abstractions.Services.Organization
{
    public interface IOrganizationDataHelper
    {
        void SetOrganizationDbContext(IOrganizationDbContext context);
        ICollection<Industry> GetOrganizationIndusties(string id);
        string? RemoveOrganizationIndustry(string id, string industryName);
        string? AddOrganizationIndustry(string id, string industryName);
    }
}
