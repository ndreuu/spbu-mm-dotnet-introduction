using Microsoft.AspNetCore.Mvc;
using Task3.DataModel;
using Task3.Services;

namespace Task3.Controllers;

/// <inheritdoc />
[ApiController]
[Route("/api/")]
public class WeatherForecastController : ControllerBase
{
	private const string Lat = "59.9343";
	private const string Lon = "30.3351";
	private readonly string? _apiKeyTomorrow = Environment.GetEnvironmentVariable("TOMORROW_API_KEY"); 
	private readonly string? _apiKeyStormglass = Environment.GetEnvironmentVariable("STORMGLASS_API_KEY"); 
	private Dictionary<string, DataPretty> GetWeatherViaApi(string? source)
	{
		Dictionary<string, DataPretty> acc = new Dictionary<string, DataPretty>();
		if (source == "Stormglass")
		{
			Stormglass service = new Stormglass(new UrlArgs(Lat, Lon, apiKey: _apiKeyStormglass));
			acc.Add(service.Name(), new DataPretty(service.Info()));
			return acc;
		}
		if (source == "Tomorrow")
		{
			Tomorrow service = new Tomorrow(new UrlArgs(Lat, Lon, apiKey: _apiKeyTomorrow));
			acc.Add(service.Name(), new DataPretty(service.Info()));
			return acc;
		}
		if (source == "All")
		{
			Stormglass service1 = new Stormglass(new UrlArgs(Lat, Lon, apiKey: _apiKeyStormglass));
			Tomorrow service2 = new Tomorrow(new UrlArgs(Lat, Lon, apiKey: _apiKeyTomorrow));
			acc.Add(service1.Name(), new DataPretty(service1.Info()));
			acc.Add(service2.Name(), new DataPretty(service2.Info()));
			return acc;

		}

		throw new Exception("unknown service");
	}	

	/// <summary>
	/// Get weather from services.
	/// </summary>
	/// <param name="source">Sources of weather are Stormglass, Tomorrow. To get weather from both: use All.
	/// If at least one not available then the exception will be return.</param>
	/// <returns>Returns information of weather form chosen service(s).</returns>
	[HttpGet("service")]
	public IActionResult GetWeather(String? source)
	{
		try
		{
			var weatherData = GetWeatherViaApi(source);
			return Ok(weatherData);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}	
}