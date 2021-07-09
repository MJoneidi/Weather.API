using System;
using System.Collections.Generic;

namespace Weather.API.Models.Dto
{
    public class WeatherForecastResponse
    {
        public WeatherForecastResponse()
        {
            WeatherInfos = new List<WeatherInfo>();
        }
        public string City { get; set; }
        public string Summary { get; set; }
        public List<WeatherInfo> WeatherInfos { get; set; }
    }

    public class WeatherInfo
    {
        public int Humidity { get; set; }
        public double TemperatureC => (TemperatureF - 32) * 5 / 9;
        public double TemperatureF { get; set; }
        public double WindSpeed { get; set; }
        public DateTime Date { get; set; }
    }
}
