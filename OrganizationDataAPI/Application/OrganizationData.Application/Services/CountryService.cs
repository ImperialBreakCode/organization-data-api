﻿using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.DTO.Country;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services
{
    internal class CountryService : ICountryService
    {
        private readonly IOrganizationDbContext _organizationDbContext;
        private readonly IDataFilter _dataFilter;

        public CountryService(IOrganizationData organizationData, IDataFilter dataFilter)
        {
            _organizationDbContext = organizationData.DbContext;
            _dataFilter = dataFilter;
        }

        public string AddCountry(CreateCountryRequestDTO createCountryDTO)
        {
            var countries = _organizationDbContext.Country.GetCountriesByName(createCountryDTO.CountryName);
            var country = _dataFilter.FilterData(countries).FirstOrDefault();
            if (country is not null)
            {
                return ResponseMessages.CountryWithNameConflict;
            }

            Country newCountry = new Country()
            {
                CountryName = createCountryDTO.CountryName
            };

            _organizationDbContext.Country.Insert(newCountry);
            _organizationDbContext.SaveChanges();

            return ResponseMessages.CountryCreated;
        }

        public string DeleteCountryById(string id)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            country!.DeletedAt = DateTime.UtcNow;
            _organizationDbContext.Country.Update(country);
            _organizationDbContext.SaveChanges();

            return ResponseMessages.CountryDeleted;
        }

        public string UpdateCountryName(string id, UpdateCountryNameRequestDTO updateCountryDTO)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            country!.CountryName = updateCountryDTO.CountryName;

            _organizationDbContext.Country.Update(country);
            _organizationDbContext.SaveChanges();

            return ResponseMessages.CountryUpdated;
        }

        public ServiceGetResult<GetCountryResponse> GetCountryById(string id)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);

            if (filterResult.Success)
            {
                return new ServiceGetResult<GetCountryResponse>()
                {
                    Result = new GetCountryResponse(country!.Id, country.CountryName, country.CreatedAt)
                };
            }

            return new ServiceGetResult<GetCountryResponse>()
            {
                ErrorMessage = filterResult.ErrorMessage!
            };
        }

        public ServiceGetResult<GetCountryResponse> GetCountryByName(string name)
        {
            var countries = _organizationDbContext.Country.GetCountriesByName(name);
            Country? country = _dataFilter.FilterData(countries).FirstOrDefault();
            if (country is null)
            {
                return new ServiceGetResult<GetCountryResponse>()
                {
                    ErrorMessage = ResponseMessages.DataNotFound
                };
            }

            return new ServiceGetResult<GetCountryResponse>()
            {
                Result = new GetCountryResponse(country.Id, country.CountryName, country.CreatedAt)
            };
        }
    }
}
