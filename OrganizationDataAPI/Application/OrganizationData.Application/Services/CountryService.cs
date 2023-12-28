using OrganizationData.Application.Abstractions.Data;
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
            var country = _organizationDbContext.Country.GetNonDeletedCountryByName(createCountryDTO.CountryName);
            var result = _dataFilter.CheckSingle(country);
            if (result.Success)
            {
                return ServiceMessages.CountryNameConflict;
            }

            Country newCountry = new Country()
            {
                CountryName = createCountryDTO.CountryName
            };

            _organizationDbContext.Country.Insert(newCountry);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.CountryCreated;
        }

        public string DeleteCountryById(string id)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            _organizationDbContext.Country.SoftDelete(country!);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.CountryDeleted;
        }

        public string UpdateCountryName(string id, UpdateCountryNameRequestDTO updateCountryDTO)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            var countryWithTheChosenName = _organizationDbContext.Country.GetNonDeletedCountryByName(updateCountryDTO.CountryName);
            filterResult = _dataFilter.CheckSingle(countryWithTheChosenName);
            if (filterResult.Success)
            {
                return ServiceMessages.CountryNameConflict;
            }

            country!.CountryName = updateCountryDTO.CountryName;

            _organizationDbContext.Country.Update(country);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.CountryUpdated;
        }

        public ServiceGetResult<GetCountryResponse> GetCountryById(string id)
        {
            var country = _organizationDbContext.Country.GetById(id);
            var filterResult = _dataFilter.CheckSingle(country);

            return ReturnGetResult(country, filterResult);
        }

        public ServiceGetResult<GetCountryResponse> GetCountryByName(string name)
        {
            var country = _organizationDbContext.Country.GetNonDeletedCountryByName(name);
            var result = _dataFilter.CheckSingle(country);
            
            return ReturnGetResult(country, result);
        }

        private ServiceGetResult<GetCountryResponse> ReturnGetResult(Country? country, FilterResult result)
        {
            if (result.Success)
            {
                return new ServiceGetResult<GetCountryResponse>()
                {
                    Result = new GetCountryResponse(country!.Id, country.CountryName, country.CreatedAt)
                };
            }

            return new ServiceGetResult<GetCountryResponse>()
            {
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}
