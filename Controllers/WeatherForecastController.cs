using Microsoft.AspNetCore.Mvc;

namespace azure_app_configuration_docker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return configuration.GetValue<string>("Hello")!;
        }
    }
}
