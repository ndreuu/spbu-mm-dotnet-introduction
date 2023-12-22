namespace Task3.DataModel;

public abstract class Data
{
	double TemperatureC { get; set; }
	public double TemperatureF => TemperatureC * 1.8 + 32;
	public int? Cloudiness { get; set; }
	public int? Humidity { get; set; }
	public int? Precipitation { get; set; }
	public string? WindDirection { get; set; }
	public double? WindSpeed { get; set; }
}