using AutoMapper;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.DTO.Stats;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.QueryStatsResults;

namespace OrganizationData.Application.Mapper
{
    internal class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<Organization, GetOrganizationResponseDTO>();
            CreateMap<UpdateOrganizationRequestDTO, Organization>();
            CreateMap<CreateOrganizationRequestDTO, Organization>();

            CreateMap<OrganizationCsvData, Organization>()
                .ForMember(dest => dest.CountryId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<OrganizationCountByCountry, OrganizationCountByCountryDTO>();
            CreateMap<OrganizationCountByIndustry, OrganizationCountByIndustryDTO>();
        }
    }
}
