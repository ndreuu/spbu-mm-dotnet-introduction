using Newtonsoft.Json;

namespace Task3.DataModel;


public class TomorrowData : Data
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Daily
    {
        public Values values { get; set; }
    }

    public class Root
    {
        public Timelines timelines { get; set; }
    }

    public class Timelines
    {
        public List<Daily> daily { get; set; }
    }

    public class Values
    {
        public double cloudCoverAvg { get; set; }
        public double humidityAvg { get; set; }
        public double precipitationProbabilityAvg { get; set; }
        public double temperatureAvg { get; set; }
        public double windDirectionAvg { get; set; }
        public double windSpeedAvg { get; set; }
    }

    public TomorrowData(string? response)
    {
        var ast = JsonConvert.DeserializeObject<Root>(response);
        if (ast is not null)
        {
            TemperatureC = ast.timelines.daily[0].values.temperatureAvg;
            Precipitation = ast.timelines.daily[0].values.precipitationProbabilityAvg;
            Cloudiness = ast.timelines.daily[0].values.cloudCoverAvg;
            Humidity = ast.timelines.daily[0].values.humidityAvg;
            SetWindDirection(ast.timelines.daily[0].values.windDirectionAvg);
            WindSpeed = ast.timelines.daily[0].values.windSpeedAvg;
        }
        else
        {
            throw new ArgumentException("Invalid OpenWeatherMap's response structure");
        }
    }



}