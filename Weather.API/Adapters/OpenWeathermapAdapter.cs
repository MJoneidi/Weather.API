using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.API.Configurations;
using Weather.API.Infrastructure;
using Weather.API.Models.Dto;

namespace Weather.API.Adapters
{
    public class OpenWeathermapAdapter: IWeatherServiceAdapter
    {
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IRequestSender<WeatherResponse> _requestSender;


        public OpenWeathermapAdapter(IApplicationConfiguration applicationConfiguration, IRequestSender<WeatherResponse> requestSender)
        {
            _applicationConfiguration = applicationConfiguration;
            _requestSender = requestSender;
        }

        public async Task<WeatherResponse> SendRequestAsync(WeatherRequest request)
        {
            string jsonString = JsonConvert.SerializeObject(request);

            return await _requestSender.SendAsync(_applicationConfiguration.ServiceUrl, "", jsonString);
        }

    }
}
