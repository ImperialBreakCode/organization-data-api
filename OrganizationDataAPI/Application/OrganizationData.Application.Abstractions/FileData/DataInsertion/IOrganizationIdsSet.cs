using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.Abstractions.FileData.DataInsertion
{
    public interface IOrganizationIdsSet
    {
        void LoadData(IOrganizationDbContext context);
        bool ContainsId(string id);
        void AddId(string id);
        void RemoveId(string id);
    }
}
