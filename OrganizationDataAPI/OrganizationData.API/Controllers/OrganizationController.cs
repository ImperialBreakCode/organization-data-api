using Microsoft.AspNetCore.Mvc;
using OrganizationData.API.Extensions;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.ResponseMessage;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("GetByOrganizationId/{organizationId}")]
        public IActionResult GetByOrganizationId(string organizationId)
        {
            var result = _organizationService.GetOrganizationByOrganizationId(organizationId);
            if (result.ErrorMessage is not null)
            {
                return this.ParseAndReturnMessage(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpPost("CreateOrganization")]
        public IActionResult CreateOrganization(CreateOrganizationRequestDTO dto)
        {
            var result = _organizationService.CreateOrganization(dto);
            var responseType = ResponseMessageParser.Parse(result);
            if (responseType == ResponseType.Created)
            {
                var organization = _organizationService.GetOrganizationByOrganizationId(dto.OrganizationId).Result;
                return Created($"/api/Organization/GetByOrganizationId/{organization.OrganizationId}", organization);
            }

            return this.ParseAndReturnMessage(result);
        }

        [HttpPut("UpdateByOrganizationId/{organizationId}")]
        public IActionResult UpdateByOrganizationId([FromRoute] string organizationId, [FromBody] UpdateOrganizationRequestDTO updateDTO)
        {
            var result = _organizationService.UpdateOrganization(organizationId, updateDTO);
            return this.ParseAndReturnMessage(result);
        }

        [HttpPut("AddIndustry")]
        public IActionResult AddIndustry(AddIndustryRequestDTO addIndustryDTO)
        {
            var result = _organizationService.AddIndustry(addIndustryDTO);
            return this.ParseAndReturnMessage(result);
        }

        [HttpPut("RemoveIndustry")]
        public IActionResult RemoveIndustry(RemoveIndustryRequestDTO removeIndustryDTO)
        {
            var result = _organizationService.RemoveIndustry(removeIndustryDTO);
            return this.ParseAndReturnMessage(result);
        }

        [HttpDelete("DeleteByOrganizationId/{organizationId}")]
        public IActionResult DeleteByOrganizationId(string organizationId)
        {
            var result = _organizationService.DeleteOrganization(organizationId);
            return this.ParseAndReturnMessage(result);
        }
    }
}
