using Newtonsoft.Json;

namespace Task3.DataModel;

public class StormglassData : Data
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class AirTemperature
	{
		public double noaa { get; set; }
	}

	public class CloudCover
	{
		public double noaa { get; set; }
	}

	public class Hour
	{
		public AirTemperature airTemperature { get; set; }
		public CloudCover cloudCover { get; set; }
		public Humidity humidity { get; set; }
		public Precipitation precipitation { get; set; }
		public DateTime time { get; set; }
		public WindDirection windDirection { get; set; }
		public WindSpeed windSpeed { get; set; }
	}

	public class Humidity
	{
		public double noaa { get; set; }
	}

	public class Meta
	{
		public int cost { get; set; }
		public int dailyQuota { get; set; }
		public string end { get; set; }
		public double lat { get; set; }
		public double lng { get; set; }
		public List<string> @params { get; set; }
		public int requestCount { get; set; }
		public List<string> source { get; set; }
		public string start { get; set; }
	}

	public class Precipitation
	{
		public double noaa { get; set; }
	}

	public class Root
	{
		public List<Hour> hours { get; set; }
		public Meta meta { get; set; }
	}

	public class WindDirection
	{
		public double noaa { get; set; }
	}

	public class WindSpeed
	{
		public double noaa { get; set; }
	}



	public StormglassData(string? response)
	{
		var ast = JsonConvert.DeserializeObject<Root>(response);
		if (ast is not null)
		{
			base.TemperatureC = ast.hours[0].airTemperature.noaa;
			base.Precipitation = ast.hours[0].precipitation.noaa;
			base.Cloudiness = ast.hours[0].cloudCover.noaa;
			base.Humidity = ast.hours[0].humidity.noaa;
			base.SetWindDirection(ast.hours[0].windDirection.noaa);
			base.WindSpeed = ast.hours[0].windSpeed.noaa;
		}
		else
		{
			throw new ArgumentException("Invalid Stormglass's response structure");
		}
	}

}