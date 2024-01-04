using AutoMapper;
using OrganizationData.Application.DTO.Country;
using OrganizationData.Data.Entities;

namespace OrganizationData.Application.Mapper
{
    internal class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, GetCountryResponse>();
        }
    }
}
