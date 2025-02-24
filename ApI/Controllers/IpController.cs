using System.Net;
using DataAccessLayer.Repositories.BlockedCountries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AttempsServices;
using Services.Conteries;
using Services.GeoLocationService;
using Services.LocationService;

namespace ApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpController : ControllerBase
    {
        private readonly ILocationService _geoLocationService;
        private readonly IBlockedCountryService _blockedCountryService;
        private readonly IBlockedAttepmsService _blockedAttepmsService;

        public IpController(ILocationService geoLocationService,IBlockedCountryService blockedCountryService,IBlockedAttepmsService blockedAttepmsService)
        {
            _geoLocationService = geoLocationService;
            this._blockedCountryService = blockedCountryService;
            this._blockedAttepmsService = blockedAttepmsService;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> LookupIp(string? ipAddress)
        { // If ipAddress is not provided, get the caller's IP address
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            // Call the third-party API to get country details
            var country = await _geoLocationService.GetCountryByIpAsync(ipAddress);
            return Ok(country);
        }

        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
           
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            

            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            var country = await _geoLocationService.GetCountryByIpAsync(ip);
            var isBlocked = await _blockedCountryService.IsBlockedAsync(country.Code);

            // Log the attempt
            await _blockedAttepmsService.LogAttemptAsync(ip, country.Code, isBlocked, userAgent);

            return Ok(new { IsBlocked = isBlocked });
        }
    }
}
