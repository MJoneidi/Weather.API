using System;
using System.Threading.Tasks;
using Weather.API.Adapters;
using Weather.API.Models.Dto;
using Weather.API.Models.Entities;
using Weather.API.Persistence;

namespace Weather.API.Processors
{
    public class WeatherProcessors : IWeatherProcessors
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
            var result = new WeatherForecastResponse();
            var response = await _weatherServiceAdapter.SendRequestAsync(requestUri);

            if (response != null)
            {
                result.City = response.city.name;
                foreach (var item in response.list)
                {
                    //limit to 5 days
                    //if (Convert.ToDateTime(item.dt_txt) > DateTime.Now.AddDays(5))
                    //    break;
                    await _repository.SaveWeatherHistoryAsync(new WeatherHistory()
                    {
                        CityID = response.city.id,
                        Date = Convert.ToDateTime(item.dt_txt),
                        Humidity = item.main.humidity,
                        Temperature = item.main.temp
                    });

                    var info = new WeatherInfo() { Date = Convert.ToDateTime(item.dt_txt), Humidity = item.main.humidity, TemperatureF = item.main.temp, WindSpeed = item.wind.speed };
                    result.WeatherInfos.Add(info);
                }

            }
            return await Task.FromResult(result);
        }
    }
}
