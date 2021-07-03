using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Models.Dto;

namespace Weather.API.Adapters
{
    public interface IWeatherServiceAdapter
    {
        Task<dynamic> SendRequestAsync(string requestUri);
    }
}
