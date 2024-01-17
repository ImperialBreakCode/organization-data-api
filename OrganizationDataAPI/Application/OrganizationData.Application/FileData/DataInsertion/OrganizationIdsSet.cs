using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.FileData.DataInsertion
{
    internal class OrganizationIdsSet : IOrganizationIdsSet
    {
        private HashSet<string> _organizationIdsSet;

        public void AddId(string id)
        {
            _organizationIdsSet.Add(id);
        }

        public bool ContainsId(string id)
        {
            return _organizationIdsSet.Contains(id);
        }

        public void LoadData(IOrganizationDbContext context)
        {
            _organizationIdsSet = context.Organization.GetAllOrganizationIds().ToHashSet();
        }

        public void RemoveId(string id)
        {
            _organizationIdsSet.Remove(id);
        }
    }
}
