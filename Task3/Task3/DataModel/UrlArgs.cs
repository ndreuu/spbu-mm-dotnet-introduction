namespace Task3.DataModel;

public class UrlArgs
{
	public UrlArgs(string lat, string lon, string? apiKey)
	{
		Lat = lat;
		Lon = lon;
		try
		{
			ApiKey = apiKey;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public string Lat { get; set; }
	public string Lon { get; set; }
	public string? ApiKey { get; set; }
}