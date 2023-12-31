﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationData.API.Constants;
using OrganizationData.API.Extensions;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.DTO.Country;
using OrganizationData.Application.ResponseMessage;

namespace OrganizationData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("GetCountryById/{id}")]
        public IActionResult GetById(string id)
        {
            var result = _countryService.GetCountryById(id);

            if (result.ErrorMessage is not null)
            {
                return this.ParseAndReturnMessage(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [HttpGet("GetCountryByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var result = _countryService.GetCountryByName(name);

            if (result.ErrorMessage is not null)
            {
                return this.ParseAndReturnMessage(result.ErrorMessage);
            }

            return Ok(result.Result);
        }

        [Authorize(Policy = ApiScopes.WriteScope)]
        [HttpPost("CreateCountry")]
        public IActionResult CreateCountry(CreateCountryRequestDTO dto)
        {
            var result = _countryService.AddCountry(dto);
            ResponseType responseType = ResponseMessageParser.Parse(result);

            if (responseType == ResponseType.Created)
            {
                var getDto = _countryService.GetCountryByName(dto.CountryName).Result;
                return Created($"/api/Country/GetCountryById/{getDto.Id}", getDto);
            }

            return this.ParseAndReturnMessage(result);
        }

        [Authorize(Policy = ApiScopes.WriteScope)]
        [HttpPut("UpdateCountryName/{id}")]
        public IActionResult UpdateCountryName([FromRoute] string id, [FromBody] UpdateCountryNameRequestDTO dto)
        {
            var result = _countryService.UpdateCountryName(id, dto);

            return this.ParseAndReturnMessage(result);
        }

        [Authorize(Policy = ApiScopes.FullScope)]
        [HttpDelete("DeleteCountryById/{id}")]
        public IActionResult DeleteCountryById(string id)
        {
            var result = _countryService.DeleteCountryById(id);

            return this.ParseAndReturnMessage(result);
        }
    }
}
