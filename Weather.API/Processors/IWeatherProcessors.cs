using System.Threading.Tasks;
using Weather.API.Models.Dto;

namespace Weather.API.Processors
{
    public interface IWeatherProcessors
    {
        Task<WeatherForecastResponse> ProcessAsync(string requestUri);
    }
}
