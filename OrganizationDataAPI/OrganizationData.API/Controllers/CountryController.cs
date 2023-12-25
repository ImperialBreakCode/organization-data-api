using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationData.Application.DTO.Country;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        [HttpGet("GetCountryById/{id}")]
        public IActionResult GetById(string id)
        {
            return Ok();
        }

        [HttpPost("CreateCountry")]
        public IActionResult CreateCountry(CreateCountryRequestDTO dto)
        {
            return Ok();
        }

        [HttpPut("UpdateCountryName/{id}")]
        public IActionResult UpdateCountryName([FromRoute] string id, [FromBody] UpdateCountryNameRequestDTO dto)
        {
            return Ok();
        }

        [HttpDelete("DeleteCountryById/{id}")]
        public IActionResult DeleteCountryById(string id)
        {
            return Ok();
        }
    }
}
