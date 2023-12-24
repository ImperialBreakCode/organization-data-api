using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.DbManager
{
    public interface IOrganizationTableCreator
    {
        void CreateTable<T>() where T : IEntity;
    }
}
