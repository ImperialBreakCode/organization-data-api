using AutoMapper;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.DTO.Stats;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.QueryStatsResults;

namespace OrganizationData.Application.Mapper
{
    internal class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Organization, GetOrganizationResponseDTO>();
            CreateMap<UpdateOrganizationRequestDTO, Organization>();
            CreateMap<CreateOrganizationRequestDTO, Organization>();

            CreateMap<OrganizationCsvData, CreateOrganizationRequestDTO>()
                .ForMember(dest => dest.Industries, opt => opt.MapFrom(src 
                    => src.Industry.Split('/', StringSplitOptions.None).Select(i => i.Trim())));

            CreateMap<OrganizationCsvData, UpdateOrganizationRequestDTO>();

            CreateMap<OrganizationCountByCountry, OrganizationCountByCountryDTO>();
            CreateMap<OrganizationCountByIndustry, OrganizationCountByIndustryDTO>();
        }
    }
}
