using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Task3.DataModel;

namespace Tests;
delegate void EmailReceiver(Message message);

class Message
{
	public string Text { get; }
	public Message(string text) => Text = text;
	public virtual void Print() => Console.WriteLine($"Message: {Text}");
}
class EmailMessage: Message
{
	public EmailMessage(string text): base(text) { }
	public override void Print() => Console.WriteLine($"Email: {Text}");
}
class SmsMessage : Message
{
	public SmsMessage(string text) : base(text) { }
	public override void Print() => Console.WriteLine($"Sms: {Text}");
}



public class Tests
{
	private class DataNull : Data
	{
		public double? TemperatureC = null;
		public double? TemperatureF = null;
		public double? Precipitation = null;
		public double? Cloudiness = null;
		public double? Humidity = null;
		public double? WindSpeed = null;
		public string? WindDirection = null;
	}

	[Test]
	public void ResponseEmpty()
	{
		var response = new Response("", null);
		Assert.IsFalse(response.IsSuccess);
		Assert.That("UriFormatException occurred: Invalid URI: The URI is empty." == response.ErrorMessage);
	}
	
	[Test]
	public static void NoData()
	{
		var prettyData = new DataPretty(new DataNull());
		var fields = new List<string>
		{
			prettyData.TemperatureC, prettyData.TemperatureF, prettyData.WindDirection, prettyData.WindSpeed,
			prettyData.Cloudiness, prettyData.Humidity, prettyData.Precipitation
		};
		var noDataCount = fields.Count(x => x == "No data");
		Assert.That(fields.Count == noDataCount);
	}
}