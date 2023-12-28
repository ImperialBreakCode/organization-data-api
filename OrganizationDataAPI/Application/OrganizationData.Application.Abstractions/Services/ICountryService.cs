using OrganizationData.Application.DTO.Country;

namespace OrganizationData.Application.Abstractions.Services
{
    public interface ICountryService
    {
        string AddCountry(CreateCountryRequestDTO createCountryDTO);
        string UpdateCountryName(string id, UpdateCountryNameRequestDTO updateCountryDTO);
        string DeleteCountryById(string id);
        ServiceGetResult<GetCountryResponse> GetCountryById(string id);
        ServiceGetResult<GetCountryResponse> GetCountryByName(string name);
    }
}
