using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shortha.Controllers
{
    [ApiController]
    [Route("api/app")]
    public class AppController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            this._logger.LogInformation("Get method called");
            return Ok(result);

        }
    }
}
