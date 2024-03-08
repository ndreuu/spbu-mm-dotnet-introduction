using Microsoft.AspNetCore.Mvc;
using Task3.DataModel;
using Task3.Services;

namespace Task3.Controllers;


[ApiController]
[Route("/api/")]
public class WeatherForecastController : ControllerBase
{
	private const string Lat = "59.9343";
	private const string Lon = "30.3351";
	private readonly string? apiKeyTomorrow = Environment.GetEnvironmentVariable("TOMORROW_API_KEY"); 
	private readonly string? apiKeyStormglass = Environment.GetEnvironmentVariable("STORMGLASS_API_KEY"); 
	private Dictionary<string, DataPretty> GetWeatherViaApi(string source)
	{
		Dictionary<string, DataPretty> acc = new Dictionary<string, DataPretty>();
		if (source == "Stormglass")
		{
			Stormglass service = new Stormglass(new UrlArgs(Lat, Lon, apiKey: apiKeyStormglass));
			acc.Add(service.Name(), new DataPretty(service.Info()));
		}
		else if (source == "Tomorrow")
		{
			Tomorrow service = new Tomorrow(new UrlArgs(Lat, Lon, apiKey: apiKeyTomorrow));
			acc.Add(service.Name(), new DataPretty(service.Info()));
		}
		else if (source == "All")
		{
			Stormglass stormglass = new Stormglass(new UrlArgs(Lat, Lon, apiKey: apiKeyStormglass));
			Tomorrow tomorrow = new Tomorrow(new UrlArgs(Lat, Lon, apiKey: apiKeyTomorrow));
			acc.Add(stormglass.Name(), new DataPretty(stormglass.Info()));
			acc.Add(tomorrow.Name(), new DataPretty(tomorrow.Info()));
		}

		return acc;
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
			if (source is null || source != "Stormglass" && source != "Tomorrow" && source != "All")
			{
				throw new Exception("unknown service");
			}  
			var weatherData = GetWeatherViaApi(source);
			return Ok(weatherData);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}	
}