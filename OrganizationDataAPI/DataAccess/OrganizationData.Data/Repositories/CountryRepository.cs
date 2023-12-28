﻿using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Helpers;

namespace OrganizationData.Data.Repositories
{
    internal class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(ISqlConnectionWrapper sqlConnectionWrapper) 
            : base(sqlConnectionWrapper)
        {
        }

        public Country? GetNonDeletedCountryByName(string countryName)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(Country)}] WHERE CountryName=@countryName AND DeletedAt IS NULL");
            command.Parameters.AddWithValue("@countryName", countryName);

            return EntityConverterHelper.ToEntityCollection<Country>(command).FirstOrDefault();
        }

        public override void SoftDelete(Country entity)
        {
            var command = CreateCommand($"Update [{nameof(Organization)}] SET CountryId=NULL WHERE CountryId=@countryId");
            command.Parameters.AddWithValue("@countryId", entity.Id);
            command.ExecuteNonQuery();

            base.SoftDelete(entity);
        }
    }
}
