using System.ComponentModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task3.DataModel;

namespace Task3.Controllers;

public enum Service
{
	[Description("Not Completed")]
	Tomorrow
}

[ApiController]
[Route("/api/")]
public class WeatherForecastController : ControllerBase
{
	private Dictionary<string, IData> GetWeatherViaApi(Service source)
	{
		Dictionary<string, IData> acc = new Dictionary<string, IData>();
		if (source is Service.Tomorrow)
		{
			string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&appid=e0451b988f7dc1134f2d1574adc8a4ac&lat=59.937500&lon=30.308611";
			// RestRequest a;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

			string responce;
			using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
			{
				responce = streamReader.ReadToEnd();
			}
			
			Console.WriteLine(responce);
			IData data = new OpenWeatherMap(responce);

			acc.Add("tomorrow", data);
		}
		// else if (source.ToLower() == "tommorow")
		// {
		// 	data.AddWeatherSourceData("tommorow", SendRequest(tommorowClient));
		// }
		// else if (source.ToLower() == "all")
		// {
		// 	data.AddWeatherSourceData("stormglass", SendRequest(stormGlassClient));
		// 	data.AddWeatherSourceData("tommorow", SendRequest(tommorowClient));
		// }
		// else 
		// {
		// 	throw new Exception($"Unknown source: {source}");
		// }

		return acc;
	}	
	
	[HttpGet("service")]
	public IActionResult GetWeather(Service source)
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
	
	// [HttpGet]
	// [Route("/sources")]	
	// public Data Get()
	// {
	// 	// ?lat=39.099724&lon=-94.578331
	// 	
	// 	// Root responseTempereture = JsonConvert.DeserializeObject<Root>(responce);
	// 	
	// 	// Console.Write($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA{responseTempereture.@base}");
	// 	// Console.Write($"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA{responseTempereture.main.temp}");
	//
	//
	//
	// 	// return data;
	// }

	
	
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