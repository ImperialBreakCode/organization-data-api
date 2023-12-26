using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IRepositoryFactory
    {
        ICountryRepository CreateCountryRepository();

        IOrganizationRepository CreateOrganizationRepository();

        IRepositoryWithJunction<T, TJunction> CreateGenericRepositoryWithJunction<T, TJunction>()
            where T : class, IEntity
            where TJunction : class;
    }
}
