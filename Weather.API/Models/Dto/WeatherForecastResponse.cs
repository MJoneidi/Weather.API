using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public double TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public double WindSpeed { get; set; }
        public DateTime Date { get; set; }         
    }
}
