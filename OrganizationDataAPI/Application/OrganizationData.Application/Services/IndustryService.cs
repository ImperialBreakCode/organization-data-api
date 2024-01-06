using AutoMapper;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Factories;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.DTO.Industry;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Services
{
    internal class IndustryService : IIndustryService
    {
        private readonly IOrganizationDbContext _organizationDbContext;
        private readonly IDataFilter _dataFilter;
        private readonly IEntityFactory _entityFactory;
        private readonly IServiceGetResultFactory _serviceGetResultFactory;
        private readonly IMapper _mapper;

        public IndustryService(
            IOrganizationData organizationData,
            IDataFilter dataFilter,
            IEntityFactory entityFactory,
            IServiceGetResultFactory serviceGetResultFactory,
            IMapper mapper)
        {
            _organizationDbContext = organizationData.DbContext;
            _dataFilter = dataFilter;
            _entityFactory = entityFactory;
            _serviceGetResultFactory = serviceGetResultFactory;
            _mapper = mapper;
        }

        public string CreateIndustry(CreateIndustryRequestDTO createIndustryDTO)
        {
            var industry = _organizationDbContext.Industry.GetNonDeletedIndustryByName(createIndustryDTO.IndustryName);
            var result = _dataFilter.CheckSingle(industry);

            if (result.Success)
            {
                return ServiceMessages.IndustryNameConflict;
            }

            Industry newIndustry = _entityFactory.CreateIndustryEntity(createIndustryDTO.IndustryName);

            _organizationDbContext.Industry.Insert(newIndustry);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.IndustryCreated;
        }

        public string DeleteIndustryById(string id)
        {
            var industry = _organizationDbContext.Industry.GetById(id);
            var result = _dataFilter.CheckSingle(industry);
            if (!result.Success)
            {
                return result.ErrorMessage!;
            }

            _organizationDbContext.Industry.SoftDelete(industry!);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.IndustryDeleted;
        }

        public ServiceGetResult<GetIndustryResponseDTO> GetByIndustryById(string id)
        {
            var industry = _organizationDbContext.Industry.GetById(id);
            var result = _dataFilter.CheckSingle(industry);

            return ReturnGetResult(industry, result);
        }

        public ServiceGetResult<GetIndustryResponseDTO> GetByIndustryByName(string name)
        {
            var industry = _organizationDbContext.Industry.GetNonDeletedIndustryByName(name);
            var result = _dataFilter.CheckSingle(industry);

            return ReturnGetResult(industry, result);
        }

        public string UpdateIndustry(string id, UpdateIndustryNameRequestDTO updateIndustryDTO)
        {
            var industry = _organizationDbContext.Industry.GetById(id);
            var filterResult = _dataFilter.CheckSingle(industry);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            var industryWithTheChosenName = _organizationDbContext.Industry.GetNonDeletedIndustryByName(updateIndustryDTO.IndustryName);
            filterResult = _dataFilter.CheckSingle(industryWithTheChosenName);
            if (filterResult.Success)
            {
                return ServiceMessages.IndustryNameConflict;
            }

            industry!.IndustryName = updateIndustryDTO.IndustryName;

            _organizationDbContext.Industry.Update(industry);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.IndustryUpdated;
        }

        private ServiceGetResult<GetIndustryResponseDTO> ReturnGetResult(Industry? industry, FilterResult result)
        {
            if (result.Success)
            {
                return _serviceGetResultFactory.CreateGetServiceResult(_mapper.Map<GetIndustryResponseDTO>(industry), null);
            }

            return _serviceGetResultFactory.CreateGetServiceResult<GetIndustryResponseDTO>(null, result.ErrorMessage);
        }
    }
}
