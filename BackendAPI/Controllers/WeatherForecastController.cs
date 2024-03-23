using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly AppConfiguration _appConfiguration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOptions<AppConfiguration> options)
        {
            _logger = logger;
            _appConfiguration = options.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(_appConfiguration);
        }
    }
}
