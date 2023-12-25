using OrganizationData.Application.DTO.Country;

namespace OrganizationData.Application.Abstractions.Services.Country
{
    public interface ICountryService
    {
        string AddCountry(CreateCountryRequestDTO createCountryDTO);
        string UpdateCountryName(UpdateCountryNameRequestDTO updateCountryDTO);
        string DeleteCountryById(string id);
        GetCountryResponse? GetCountryById(string id);
    }
}
