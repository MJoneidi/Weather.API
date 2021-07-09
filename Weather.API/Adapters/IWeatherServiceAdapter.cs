using System.Threading.Tasks;

namespace Weather.API.Adapters
{
    public interface IWeatherServiceAdapter
    {
        Task<dynamic> SendRequestAsync(string requestUri);
    }
}
