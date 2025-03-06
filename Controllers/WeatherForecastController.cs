using Microsoft.AspNetCore.Mvc;
using taiwanese_translator_api.Models;

namespace taiwanese_translator_api.Controllers
{
	[ApiController]
	[Route("[controller]")] // 這樣 API 會對應到 /weather
	public class WeatherController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		[HttpGet] // GET /weather
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index =>
				new WeatherForecast
				(
					DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
					Random.Shared.Next(-20, 55),
					Summaries[Random.Shared.Next(Summaries.Length)]
				))
				.ToArray();
		}
	}
}
