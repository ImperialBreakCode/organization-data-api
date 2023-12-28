using OrganizationData.Application.DTO.Industry;

namespace OrganizationData.Application.Abstractions.Services
{
    public interface IIndustryService
    {
        string CreateIndustry(CreateIndustryRequestDTO createIndustryDTO);
        string UpdateIndustry(string id, UpdateIndustryNameRequestDTO updateIndustryDTO);
        string DeleteIndustryById(string id);
        ServiceGetResult<GetIndustryResponseDTO> GetByIndustryByName(string name);
        ServiceGetResult<GetIndustryResponseDTO> GetByIndustryById(string id);
    }
}
