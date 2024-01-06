using AutoMapper;
using OrganizationData.Application.DTO.Industry;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Mapper
{
    internal class IndustryMappingProfile : Profile
    {
        public IndustryMappingProfile()
        {
            CreateMap<Industry, GetIndustryResponseDTO>();
        }
    }
}
