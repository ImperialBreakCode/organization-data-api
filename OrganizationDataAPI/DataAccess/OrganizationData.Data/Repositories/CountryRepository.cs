using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    internal class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction) 
            : base(sqlConnection, sqlTransaction)
        {
        }

        public ICollection<Country> GetCountriesByName(string countryName)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(Country)}] WHERE CountryName=@countryName");
            command.Parameters.AddWithValue("@countryName", countryName);

            return EntityConverterHelper.ToEntityCollection<Country>(command);
        }
    }
}
