using AutoMapper;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.DTO.Stats;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services
{
    internal class StatsService : IStatsService
    {
        private readonly IOrganizationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOrganizationDataHelper _organizationDataHelper;

        public StatsService(IOrganizationData organizationData, IMapper mapper, IOrganizationDataHelper organizationDataHelper)
        {
            _context = organizationData.DbContext;
            _mapper = mapper;

            _organizationDataHelper = organizationDataHelper;
            _organizationDataHelper.SetOrganizationDbContext(_context);
        }

        public ICollection<OrganizationCountByCountryDTO> GetOrganizationCountByCountry()
        {
            var statRepoResult = _context.Stats.GetOrganizationCountByCountries();

            return statRepoResult
                .Select(s => _mapper.Map<OrganizationCountByCountryDTO>(s))
                .ToList();
        }

        public ICollection<OrganizationCountByIndustryDTO> GetOrganizationCountByIndustry()
        {
            var statRepoResult = _context.Stats.GetOrganizationCountByIndustries();

            return statRepoResult
                .Select(s => _mapper.Map<OrganizationCountByIndustryDTO>(s))
                .ToList();
        }

        public ICollection<GetOrganizationResponseDTO> GetTopTenOrganizationsWithMostWorkers()
        {
            var organizations = _context.Stats.GetTopTenOrganizationsWithMostWorkers();

            return ProccesGetOrganizationDTO(organizations);
        }

        public ICollection<GetOrganizationResponseDTO> GetTopTenYoungestOrganizations()
        {
            var organizations = _context.Stats.GetTopTenYoungestOrganizations();

            return ProccesGetOrganizationDTO(organizations);
        }

        private ICollection<GetOrganizationResponseDTO> ProccesGetOrganizationDTO(ICollection<Organization> organizations)
        {
            var serviceResult = organizations
                .Select(o => _mapper.Map<GetOrganizationResponseDTO>(o))
                .ToList();

            foreach (var item in serviceResult)
            {
                item.Industries = _organizationDataHelper
                    .GetOrganizationIndusties(item.Id)
                    .Select(i => i.IndustryName)
                    .ToList();

                string countryId = organizations.Where(o => o.Id == item.Id).First().CountryId;
                item.Country = _context.Country.GetById(countryId)!.CountryName;
            }

            return serviceResult;
        }
    }
}
