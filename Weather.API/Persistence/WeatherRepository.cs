using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Models.Entities;

namespace Weather.API.Persistence
{
    public class WeatherRepository : IWeatherRepository
    {
        protected WeatherDbContext _dbContext;

        public WeatherRepository(WeatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveWeatherHistoryAsync(WeatherHistory history)
        {
            _dbContext.WeatherHistories.Add(history);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<WeatherHistory>> GetAllWeatherHistoryAsync() => await _dbContext.WeatherHistories.ToListAsync();

        public async Task<IEnumerable<WeatherHistory>> GetAllWeatherHistoryAsync(int pageSize, int pageNumber) =>
            await _dbContext.WeatherHistories
                    .Skip((pageSize - 1) * pageNumber)
                    .Take(pageSize)
                    .ToListAsync();
    }
}
