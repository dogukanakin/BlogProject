using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrsSoftBlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpPost("log")]
        public IActionResult LogError([FromBody] string errorMessage)
        {
            _logger.LogError(errorMessage);
            return Ok();
        }
    }
}
