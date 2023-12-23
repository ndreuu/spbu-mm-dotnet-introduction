namespace Task3.DataModel;

public interface IData
{
	double TemperatureC { get; set; }
	public double TemperatureF { get; }
	public int? Cloudiness { get; set; }
	public int? Humidity { get; set; }
	public int? Precipitation { get; set; }
	public string? WindDirection { get; set; }
	public double? WindSpeed { get; set; }
}