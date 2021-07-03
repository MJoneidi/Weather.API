using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.API.Models.Entities;

namespace Weather.API.Persistence
{
    public interface IWeatherRepository
    {
        Task SaveWeatherHistoryAsync(WeatherHistory history);
        Task<IEnumerable<WeatherHistory>> GetAllWeatherHistoryAsync();
        Task<IEnumerable<WeatherHistory>> GetAllWeatherHistoryAsync(int pageSize, int pageNumber);
    }
}
