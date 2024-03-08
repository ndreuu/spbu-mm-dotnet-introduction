using System.Net;
using System.Net.Http.Headers;

namespace Task3.DataModel;

public class Response
{
	public string? Content { get; private set; }
	public bool IsSuccess { get; private set; }
	public string? ErrorMessage { get; private set; }

	public Response(string url, string? key) 
	{
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			if (key is not null)
			{
				httpWebRequest.Headers["Authorization"] = key;
			}
			
			using HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());

			Content = streamReader.ReadToEnd();
			
			IsSuccess = true;
		}
		catch (WebException webEx)
		{
			IsSuccess = false;
			ErrorMessage = $"WebException occurred: {webEx.Message}";
			if (webEx.Response != null)
			{
				using var stream = webEx.Response.GetResponseStream();
				using var reader = new StreamReader(stream);
				ErrorMessage += $"\nResponse: {reader.ReadToEnd()}";
			}
		}
		catch (UriFormatException uriEx)
		{
			IsSuccess = false;
			ErrorMessage = $"UriFormatException occurred: {uriEx.Message}";
		}
		catch (Exception ex)
		{
			IsSuccess = false;
			ErrorMessage = $"An error occurred: {ex.Message}";
		}
	}
}
