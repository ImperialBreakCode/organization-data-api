using Microsoft.AspNetCore.Mvc;
using OrganizationData.Application.Abstractions.Services;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet("GetOrganizationCountByCountries")]
        public IActionResult GetOrganizationCountByCountries()
        {
            var result = _statsService.GetOrganizationCountByCountry();
            return Ok(result);
        }

        [HttpGet("GetOrganizationCountByIndustries")]
        public IActionResult GetOrganizationCountByIndustries()
        {
            var result = _statsService.GetOrganizationCountByIndustry();
            return Ok(result);
        }

        [HttpGet("GetTopTenOrganizationsWithMostWorkers")]
        public IActionResult GetTopTenOrganizationsWithMostWorkers()
        {
            var result = _statsService.GetTopTenOrganizationsWithMostWorkers();
            return Ok(result);
        }

        [HttpGet("GetTopTenYoungestOrganizations")]
        public IActionResult GetTopTenYoungestOrganizations()
        {
            var result = _statsService.GetTopTenYoungestOrganizations();
            return Ok(result);
        }
    }
}
