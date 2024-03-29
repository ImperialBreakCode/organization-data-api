﻿using AutoMapper;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Factories;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services.OrganizationServices
{
    internal class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationDbContext _context;
        private readonly IOrganizationDataHelper _organizationDataHelper;
        private readonly IDataFilter _dataFilter;
        private readonly IMapper _mapper;
        private readonly IEntityFactory _entityFactory;
        private readonly IServiceGetResultFactory _serviceGetResultFactory;
        private readonly IOrganizationIdsSet _organizationIdsSet;

        public OrganizationService(
            IOrganizationData organizationData,
            IOrganizationDataHelper organizationDataHelper,
            IDataFilter dataFilter,
            IMapper mapper,
            IEntityFactory entityFactory,
            IServiceGetResultFactory serviceGetResultFactory,
            IOrganizationIdsSet organizationIdsSet)
        {
            _dataFilter = dataFilter;
            _context = organizationData.DbContext;
            _organizationDataHelper = organizationDataHelper;
            _mapper = mapper;
            _entityFactory = entityFactory;
            _serviceGetResultFactory = serviceGetResultFactory;
            _organizationIdsSet = organizationIdsSet;

            _organizationDataHelper.SetOrganizationDbContext(_context);
        }

        public string AddIndustry(AddIndustryRequestDTO addIndustryDTO)
        {
            Organization? organization = _context.Organization.GetByOrganizationId(addIndustryDTO.OrganizationId);
            var filterResult = _dataFilter.CheckSingle(organization);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            string? errors = _organizationDataHelper.AddOrganizationIndustry(organization!.Id, addIndustryDTO.IndustryName);
            if (errors is not null)
            {
                _context.DiscardChanges();
                return errors;
            }

            _context.SaveChanges();

            return ServiceMessages.OrganizationIndusryAdded;
        }

        public string RemoveIndustry(RemoveIndustryRequestDTO removeIndustryDTO)
        {
            Organization? organization = _context.Organization.GetByOrganizationId(removeIndustryDTO.OrganizationId);
            var filterResult = _dataFilter.CheckSingle(organization);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            string? errors = _organizationDataHelper.RemoveOrganizationIndustry(organization!.Id, removeIndustryDTO.IndustryName);
            if (errors is not null)
            {
                _context.DiscardChanges();
                return errors;
            }

            _context.SaveChanges();

            return ServiceMessages.OrganizationIndusryRemoved;
        }

        public string CreateOrganization(CreateOrganizationRequestDTO createDTO)
        {
            if (_organizationIdsSet.ContainsId(createDTO.OrganizationId))
            {
                return ServiceMessages.OrganizationIdConflict;
            }

            var country = _context.Country.GetNonDeletedCountryByName(createDTO.Country);
            if (country is null)
            {
                country = _entityFactory.CreateCountryEntity(createDTO.Country);

                _context.Country.Insert(country);
            }

            var organization = _mapper.Map<Organization>(createDTO);
            organization.CountryId = country.Id;
            _context.Organization.Insert(organization);

            foreach (var industryName in createDTO.Industries)
            {
                _organizationDataHelper.AddOrganizationIndustry(organization.Id, industryName);
            }

            _context.SaveChanges();

            _organizationIdsSet.AddId(organization.OrganizationId);

            return ServiceMessages.OrganizationCreated;
        }
        
        public string UpdateOrganization(string organizationId, UpdateOrganizationRequestDTO updateDTO)
        {
            if (organizationId != updateDTO.OrganizationId && _organizationIdsSet.ContainsId(updateDTO.OrganizationId))
            {
                return ServiceMessages.OrganizationIdConflict;
            }

            Organization? organization = _context.Organization.GetByOrganizationId(organizationId);
            var filterResult = _dataFilter.CheckSingle(organization);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            Country? country = _context.Country.GetNonDeletedCountryByName(updateDTO.Country);
            if (country is null)
            {
                country = _entityFactory.CreateCountryEntity(updateDTO.Country);

                _context.Country.Insert(country);
            }

            _mapper.Map(updateDTO, organization);
            organization!.CountryId = country.Id;
            _context.Organization.Update(organization!);
            _context.SaveChanges();

            _organizationIdsSet.RemoveId(organizationId);
            _organizationIdsSet.AddId(updateDTO.OrganizationId);

            return ServiceMessages.OrganizationUpdated;
        }

        public string DeleteOrganization(string organizationId)
        {
            Organization? organization = _context.Organization.GetByOrganizationId(organizationId);
            var filterResult = _dataFilter.CheckSingle(organization);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            _context.Organization.SoftDelete(organization!);
            _context.SaveChanges();

            _organizationIdsSet.RemoveId(organizationId);

            return ServiceMessages.OrganizationDeleted;
        }

        public ServiceGetResult<GetOrganizationResponseDTO> GetOrganizationByOrganizationId(string organizationId)
        {
            Organization? organization = _context.Organization.GetByOrganizationId(organizationId);
            var filterResult = _dataFilter.CheckSingle(organization);
            if (!filterResult.Success)
            {
                return _serviceGetResultFactory.CreateGetServiceResult<GetOrganizationResponseDTO>(null, filterResult.ErrorMessage);
            }

            var result = _mapper.Map<GetOrganizationResponseDTO>(organization);

            if (organization!.CountryId is not null)
            {
                result.Country = _context.Country.GetById(organization.CountryId)!.CountryName;
            }
            
            result.Industries = _organizationDataHelper
                .GetOrganizationIndusties(organization.Id)
                .Select(industry => industry.IndustryName)
                .ToList();

            return _serviceGetResultFactory.CreateGetServiceResult(result, null);
        }
    }
}
