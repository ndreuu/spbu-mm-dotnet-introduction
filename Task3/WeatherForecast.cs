namespace Task3;

public class WeatherForecast
{
	public DateTime Date { get; set; }
	public double kek { get; set; }
	public string kekl { get; set; }

	public int TemperatureC { get; set; }

	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

	public string? Summary { get; set; }
}