using Microsoft.EntityFrameworkCore;
using Weather.API.Models.Entities;

namespace Weather.API.Persistence
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherHistory> WeatherHistories { get; set; }
    }
}
