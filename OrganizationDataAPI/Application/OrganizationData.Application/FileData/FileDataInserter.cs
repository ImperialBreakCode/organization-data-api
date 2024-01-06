using AutoMapper;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.DTO.Organization;

namespace OrganizationData.Application.FileData
{
    internal class FileDataInserter : IFileDataInserter
    {
        private readonly IOrganizationService _organizationService;
        private readonly IMapper _mapper;

        public FileDataInserter(IOrganizationService organizationService, IMapper mapper)
        {
            _organizationService = organizationService;
            _mapper = mapper;
        }

        public void SaveData(OrganizationCsvData data)
        {
            var result = _organizationService.GetOrganizationByOrganizationId(data.OrganizationId);

            if (result.ErrorMessage is not null)
            {
                CreateData(data);
            }
            else
            {
                UpdateData(data, result.Result);
            }
        }

        private void UpdateData(OrganizationCsvData updatedData, GetOrganizationResponseDTO currentData)
        {
            _organizationService.UpdateOrganization(
                updatedData.OrganizationId,
                _mapper.Map<UpdateOrganizationRequestDTO>(updatedData));

            ICollection<string> updatedIndustries = updatedData.Industry.Split('/').Select(i => i.Trim()).ToList();

            foreach (var industry in updatedIndustries)
            {
                if (!currentData.Industries.Contains(industry))
                {
                    _organizationService.AddIndustry(new AddIndustryRequestDTO(updatedData.OrganizationId, industry));
                }
            }

            foreach (var industry in currentData.Industries)
            {
                if (!updatedIndustries.Contains(industry))
                {
                    _organizationService.RemoveIndustry(new RemoveIndustryRequestDTO(updatedData.OrganizationId, industry));
                }
            }
        }

        private void CreateData(OrganizationCsvData data)
        {
            _organizationService.CreateOrganization(_mapper.Map<CreateOrganizationRequestDTO>(data));
        }
    }
}
