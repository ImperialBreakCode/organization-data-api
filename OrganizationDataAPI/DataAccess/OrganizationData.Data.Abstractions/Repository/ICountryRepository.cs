﻿using OrganizationData.Data.Abstractions.Repository.RepoCommon;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface ICountryRepository : IRepository<Country>
    {
        Country? GetNonDeletedCountryByName(string countryName);
    }
}
