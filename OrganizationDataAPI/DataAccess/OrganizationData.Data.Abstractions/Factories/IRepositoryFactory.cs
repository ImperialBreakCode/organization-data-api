using OrganizationData.Data.Abstractions.Repository;

namespace OrganizationData.Data.Abstractions.Factories
{
    public interface IRepositoryFactory
    {
        ICountryRepository CreateCountryRepository();
        IOrganizationRepository CreateOrganizationRepository();
        IIndustryRepository CreateIndustryRepository();
        IStatsRepository CreateStatsRepository();
    }
}
