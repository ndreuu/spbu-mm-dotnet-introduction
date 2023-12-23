using Newtonsoft.Json;

namespace Task3.DataModel;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Clouds
{
	public int all { get; set; }
}

public class Coord
{
	public double lon { get; set; }
	public double lat { get; set; }
}

public class Main
{
	public double temp { get; set; }
	public double feels_like { get; set; }
	public double temp_min { get; set; }
	public double temp_max { get; set; }
	public int pressure { get; set; }
	public int humidity { get; set; }
}

public class Root
{
	public Coord coord { get; set; }
	public List<Weather> weather { get; set; }
	public string @base { get; set; }
	public Main main { get; set; }
	public int visibility { get; set; }
	public Wind wind { get; set; }
	public Clouds clouds { get; set; }
	public int dt { get; set; }
	public Sys sys { get; set; }
	public int timezone { get; set; }
	public int id { get; set; }
	public string name { get; set; }
	public int cod { get; set; }
}

public class Sys
{
	public int type { get; set; }
	public int id { get; set; }
	public string country { get; set; }
	public int sunrise { get; set; }
	public int sunset { get; set; }
}

public class Weather
{
	public int id { get; set; }
	public string main { get; set; }
	public string description { get; set; }
	public string icon { get; set; }
}

public class Wind
{
	public double speed { get; set; }
	public int deg { get; set; }
	public double gust { get; set; }
}

public class OpenWeatherMap : Data
{
	
	public OpenWeatherMap(string response)
	{
		var ast = JsonConvert.DeserializeObject<Root>(response);
		if (ast is not null)
		{
			TemperatureC = ast.main.temp;
			Cloudiness = ast.clouds.all;
			Humidity = ast.main.humidity;
			WindDirection = GetDirectionFromDegrees(ast.wind.deg);
			WindSpeed = ast.wind.speed;
		}
		else
		{
			throw new ArgumentException("Invalid OpenWeatherMap's response structure");
		}
	}
	
	private string GetDirectionFromDegrees(double degrees)
	{
		string[] directions = { "North", "North-East", "East", "South-East", "South", "South-West", "West", "North-West", "North" };
		return directions[(int)Math.Round(((degrees % 360) / 45))];
	}

}

