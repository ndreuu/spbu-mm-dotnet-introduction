using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Task3.DataModel;

namespace Tests;

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
	public void NoData()
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