using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AttempsServices;

namespace ApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IBlockedAttepmsService _blockedAttemptsService;

        public LogsController(IBlockedAttepmsService blockedAttemptsService)
        {
            _blockedAttemptsService = blockedAttemptsService;
        }

        [HttpGet("blocked-attempts")]
        public async Task<IActionResult> GetBlockedAttempts(int pageNumber = 1, int pageSize = 10)
        {
            var attempts = await _blockedAttemptsService.GetAttemptsAsync(pageNumber, pageSize);
            return Ok(attempts);
        }
    }
}
