using AutoMapper;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Mapper
{
    internal class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Organization, GetOrganizationResponseDTO>();
            CreateMap<UpdateOrganizationRequestDTO, Organization>();
            CreateMap<CreateOrganizationRequestDTO, Organization>();
        }
    }
}
