namespace OrganizationData.Data.Abstractions.Repository.RepoCommon
{
    public interface IRepositoryFactory
    {
        ICountryRepository CreateCountryRepository();
        IOrganizationRepository CreateOrganizationRepository();
        IIndustryRepository CreateIndustryRepository();
        IStatsRepository CreateStatsRepository();
    }
}
