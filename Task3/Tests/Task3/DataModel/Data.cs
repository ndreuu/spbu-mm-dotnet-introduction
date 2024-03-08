namespace Task3.DataModel;

public abstract class Data
{
	public double? TemperatureC {set; get; }
	public double? TemperatureF => 32 + (TemperatureC * 9.0 / 5.0); 

	public double? Precipitation {set; get; }
	public double? Cloudiness {set; get; }
	public double? Humidity {set; get; }
	public double? WindSpeed	 {set; get; }
	public string? WindDirection { get; set; }

	protected void SetWindDirection(double degrees)
	{
		string?[] directions = { "North", "North-East", "East", "South-East", "South", "South-West", "West", "North-West", "North" };
		WindDirection = directions[(int)Math.Round(degrees % 360 / 45)];
	} 

	
}

