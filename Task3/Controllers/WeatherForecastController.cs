using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task3.DataModel;

namespace Task3.Controllers;

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

	
	[HttpGet(Name = "MyShit")]
	public WeatherForecast Get()
	{
		string url = "https://api.openweathermap.org/data/2.5/weather?q=London&units=metrics&appid=e0451b988f7dc1134f2d1574adc8a4ac";
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		string responce;
		using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
		{
			responce = streamReader.ReadToEnd();
		}
		Root responseTempereture = JsonConvert.DeserializeObject<Root>(responce);
		
		Console.Write($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA{responseTempereture.@base}");
		Console.Write($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA{responseTempereture.main.temp}");
		
		
		
		return new WeatherForecast
		{
			kekl = responce,
			kek = responseTempereture.main.temp,
			Date = DateTime.Now,
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		};
	}

	
	
	// [HttpGet(Name = "GetWeatherForecast")]
	// public IEnumerable<WeatherForecast> Get()
	// {
	// 	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
	// 		{
	// 			Date = DateTime.Now.AddDays(index),
	// 			TemperatureC = Random.Shared.Next(-20, 55),
	// 			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
	// 		})
	// 		.ToArray();
	// }
	
}