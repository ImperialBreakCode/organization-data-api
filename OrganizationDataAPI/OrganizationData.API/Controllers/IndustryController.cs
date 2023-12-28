using Microsoft.AspNetCore.Mvc;
using OrganizationData.API.Extensions;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.DTO.Industry;
using OrganizationData.Application.ResponseMessage;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly IIndustryService _industryService;

        public IndustryController(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        [HttpGet("GetIndustryById/{id}")]
        public IActionResult GetById(string id)
        {
            var result = _industryService.GetByIndustryById(id);

            if (result.ErrorMessage is not null)
            {
                return this.ParseAndReturnMessage(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpGet("GetIndustryByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _industryService.GetByIndustryByName(name);

            if (result.ErrorMessage is not null)
            {
                return this.ParseAndReturnMessage(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpPost("CreateIndustry")]
        public IActionResult CreateIndustry(CreateIndustryRequestDTO dto)
        {
            var result = _industryService.CreateIndustry(dto);
            ResponseType responseType = ResponseMessageParser.Parse(result);
            if (responseType == ResponseType.Created)
            {
                var industryDTO = _industryService.GetByIndustryByName(dto.IndustryName).Result;
                return Created($"/api/GetIndustryById/{industryDTO.Id}", industryDTO);
            }

            return this.ParseAndReturnMessage(result);
        }

        [HttpPut("UpdateIndustryName/{id}")]
        public IActionResult UpdateIndustryName([FromRoute] string id, [FromBody] UpdateIndustryNameRequestDTO dto)
        {
            var result = _industryService.UpdateIndustry(id, dto);
            return this.ParseAndReturnMessage(result);
        }

        [HttpDelete("DeleteIndustryById/{id}")]
        public IActionResult DeleteById(string id)
        {
            var result = _industryService.DeleteIndustryById(id);
            return this.ParseAndReturnMessage(result);
        }
    }
}
