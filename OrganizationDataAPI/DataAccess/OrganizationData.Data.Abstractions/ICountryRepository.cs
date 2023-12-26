using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions
{
    public interface ICountryRepository : IRepository<Country>
    {
        ICollection<Country> GetCountriesByName(string countryName); 
    }
}
