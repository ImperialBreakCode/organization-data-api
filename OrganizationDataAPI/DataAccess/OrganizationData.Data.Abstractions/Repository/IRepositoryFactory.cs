namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IRepositoryFactory
    {
        ICountryRepository CreateCountryRepository();
        IOrganizationRepository CreateOrganizationRepository();
        IIndustryRepository CreateIndustryRepository();
    }
}
