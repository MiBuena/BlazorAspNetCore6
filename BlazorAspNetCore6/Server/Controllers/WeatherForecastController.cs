using BlazorAspNetCore6.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAspNetCore6.Server.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("weather-forecast")]
        public async IAsyncEnumerable<WeatherForecast> GetWeatherForecastStream()
        {
            var a = GetForeCasts();

            await foreach (var item in a)
            {
                await Task.Delay(1000);
                yield return item;
            }
        }

        private async IAsyncEnumerable<WeatherForecast> GetForeCasts()
        {
            var rng = new Random();

            var a = new List<WeatherForecast>();

            a = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
                .ToList();


            foreach (var item in a)
            {
                yield return item;
            }
        }

        [HttpGet("lines")]
        public async IAsyncEnumerable<string> GetLines()
        {
            for (var i = 0; i < 10; ++i)
            {
                yield return "test\n";
                await Task.Delay(1000);
            }
        }
    }
}