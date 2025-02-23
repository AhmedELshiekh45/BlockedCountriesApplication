using System.Linq.Expressions;
using DataAccessLayer.Constants.TemporalBlock;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.BlockedCountries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Conteries;
using Services.TemporalBlockedCounteryServices;

namespace ApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IBlockedCountryService _service;
        private readonly ITemporalBlockService _temporalBlockService;

        public CountriesController(IBlockedCountryService service, ITemporalBlockService temporalBlockService)
        {
            this._service = service;
            this._temporalBlockService = temporalBlockService;
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockCountry([FromBody] string countryCode)
        {
            if (await _service.IsBlockedAsync(countryCode))
            {
                return Conflict("Country is already blocked.");
            }

            await _service.BlockAsync(countryCode);
            return Ok();
        }

        [HttpDelete("block/{countryCode}")]
        public async Task<IActionResult> UnblockCountry(string countryCode)
        {
            if (await _service.IsBlockedAsync(countryCode) == false)
            {
                return NotFound("Country not Blocked.");
            }

            await _service.UnblockAsync(countryCode);
            return Ok();
        }

        [HttpGet("blocked")]
        public async Task<IActionResult> GetBlockedCountries(int pageNumber, int pageSize = 10, string? searchTerm = null)
        {
            Expression<Func<Country, bool>> predicate = null;

            // Apply filtering if searchTerm is provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = country => country.Code.Contains(searchTerm) || country.Name.Contains(searchTerm);
            }

            var blockedCountries = await _service.GetAllBlockedAsync(pageNumber, pageSize, predicate);
            return Ok(blockedCountries);
        }



        #region TemporalBlocked

        [HttpPost("temporal-block")]
        public async Task<IActionResult> TemporarilyBlockCountry([FromBody] TemporalBlockRequest request)
        {
            if (request.DurationMinutes < 1 || request.DurationMinutes > 1440)
            {
                return BadRequest("Duration must be between 1 and 1440 minutes.");
            }

            if (await _temporalBlockService.IsCountryTemporarilyBlockedAsync(request.CountryCode))
            {
                return Conflict("Country is already temporarily blocked.");
            }

            var isBlocked = await _temporalBlockService.BlockCountryTemporarilyAsync(request.CountryCode, request.DurationMinutes);

            if (!isBlocked)
            {
                return StatusCode(500, "Failed to block country temporarily.");
            }

            return Ok("Country blocked temporarily.");
        }

        [HttpGet("temporarily-blocked")]
        public async Task<IActionResult> GetTemporarilyBlockedCountries()
        {
            var blockedCountries = await _temporalBlockService.GetTemporarilyBlockedCountriesAsync();
            return Ok(blockedCountries);
        }

        #endregion 
    }
}




