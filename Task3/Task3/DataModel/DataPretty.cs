namespace Task3.DataModel;

public class DataPretty
{
	private string MkString(double? value)
	{
		if (value is null)
		{
			return "No data"; 
		} 
		return value.ToString();
	}
	
	private string MkString(string? value)
	{
		if (value is null)
		{
			return "No data"; 
		} 
		return value;
	}
	
	public string TemperatureC { get; private set; }
	public string TemperatureF { get; private set; }
	public string Precipitation { get; private set; }
	public string Cloudiness { get; private set; }
	public string Humidity { get; private set; }
	public string WindSpeed { get; private set; }
	public string WindDirection { get; private set; }

	
	public DataPretty(Data data)
	{
		TemperatureC = MkString(data.TemperatureC);
		TemperatureF = MkString(data.TemperatureF);
		Precipitation = MkString(data.Precipitation);
		Cloudiness = MkString(data.Cloudiness);
		Humidity = MkString(data.Humidity);
		WindSpeed = MkString(data.WindSpeed);
		WindDirection = MkString(data.WindDirection);
	}
}