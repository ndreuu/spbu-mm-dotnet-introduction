using Task3.DataModel;

namespace Task3.Services;

public class Tomorrow : IService
{
	private readonly Data _info;
	private readonly string _name;

	public Tomorrow(UrlArgs args)
	{
		var response =
			new Response($"https://api.tomorrow.io/v4/weather/forecast?apikey={args.ApiKey}&location={args.Lat},{args.Lon}",
				null);

		if (!response.IsSuccess)
		{
			throw new Exception(response.ErrorMessage);
		}
		_name = "Tomorrow";
		_info = new TomorrowData(response.Content);
	}

	public Data Info()
	{
		return _info;
	}

	public string Name()
	{
		return _name;
	}

	public override string ToString()
	{
		return _name;
	}
}