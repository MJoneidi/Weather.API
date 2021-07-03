using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Adapters;
using Weather.API.Models.Dto;
using Weather.API.Models.Entities;
using Weather.API.Persistence;

namespace Weather.API.Processors
{
    public class WeatherProcessors: IWeatherProcessors
    {
        private readonly IWeatherServiceAdapter _weatherServiceAdapter;
        private readonly IWeatherRepository _repository;

        public WeatherProcessors(IWeatherServiceAdapter weatherServiceAdapter, IWeatherRepository weatherRepository)
        {
            _weatherServiceAdapter = weatherServiceAdapter;
            _repository = weatherRepository;
        }


        /// <summary>
        /// Process Weather Request by sending request to Weather map api
        /// </summary>
        /// <param name="requestUri">requestUri is city name or zip code value</param>
        /// <returns>Weather response from api</returns>
        public async Task<WeatherForecastResponse> ProcessAsync(string requestUri)
        {
            var response = await _weatherServiceAdapter.SendRequestAsync(requestUri);

            if (response != null)
                foreach (var date in response.List)
                    await _repository.SaveWeatherHistoryAsync(new WeatherHistory()
                    {
                        CityID = response.City.Id,
                        Date = Convert.ToDateTime(date.DtTxt),
                        Humidity = date.Main.Humidity,
                        Temperature = date.Main.Temp
                    });

            var result = new WeatherForecastResponse()
            {
                City = response.City.Name,
                WeatherInfos = new List<WeatherInfo>()
                {
                    new WeatherInfo(){ Date = Convert.ToDateTime(response.List[0].DtTxt), Humidity = response.List[0].Main.Humidity, TemperatureC= response.List[0].Main.Temp, WindSpeed= response.List[0].Wind.Speed },
                    new WeatherInfo(){ Date = Convert.ToDateTime(response.List[1].DtTxt), Humidity = response.List[1].Main.Humidity, TemperatureC= response.List[1].Main.Temp, WindSpeed= response.List[1].Wind.Speed },
                    new WeatherInfo(){ Date = Convert.ToDateTime(response.List[2].DtTxt), Humidity = response.List[2].Main.Humidity, TemperatureC= response.List[2].Main.Temp, WindSpeed= response.List[2].Wind.Speed },
                    new WeatherInfo(){ Date = Convert.ToDateTime(response.List[3].DtTxt), Humidity = response.List[3].Main.Humidity, TemperatureC= response.List[3].Main.Temp, WindSpeed= response.List[3].Wind.Speed },

                }
            };

            return await Task.FromResult(result);
        }
    }
}
