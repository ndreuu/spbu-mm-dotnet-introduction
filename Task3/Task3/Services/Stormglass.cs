using Task3.DataModel;

namespace Task3.Services;

public class Stormglass : IService
{
	private readonly Data _info;
	private readonly string _name;

	public Stormglass(UrlArgs args)
	{
		var date = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
		var url =
			$"https://api.stormglass.io/v2/weather/point?lat={args.Lat}&lng={args.Lon}&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed&start={date}&end={date}&source=noaa";
		_name = "Stormglass";
		var response = new Response(url, args.ApiKey);
		if (!response.IsSuccess)
		{
			throw new Exception(response.ErrorMessage);
		}
		_info = new StormglassData(response.Content);
	}

	public Data Info()
	{
		return _info;
	}

	public string Name()
	{
		return _name;
	}
}