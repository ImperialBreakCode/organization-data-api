using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationData.Application.Abstractions.Services;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService _statsService;
        private readonly IDailyCsvStatsService _dailyCsvStatsService;

        public StatsController(IStatsService statsService, IDailyCsvStatsService dailyCsvStatsService)
        {
            _statsService = statsService;
            _dailyCsvStatsService = dailyCsvStatsService;
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

        [Authorize]
        [HttpGet("GetPdfStats/{day}/{month}/{year}/{daysAhead}")]
        public IActionResult GetPdfStats(int day, int month, int year, int daysAhead)
        {
            try
            {
                var pdf = _dailyCsvStatsService.GetStatsForADate(day, month, year, daysAhead);

                if (pdf is null)
                {
                    return NotFound("No available stats for the chosen date.");
                }

                return File(pdf, "application/pdf");
            }
            catch (ArgumentOutOfRangeException)
            {

                return BadRequest("The given date is incorrect.");
            }
            
        }
    }
}
