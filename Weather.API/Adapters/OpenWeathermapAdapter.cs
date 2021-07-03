﻿using Newtonsoft.Json;
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
        private readonly IRequestSender<dynamic> _requestSender;


        public OpenWeathermapAdapter(IApplicationConfiguration applicationConfiguration, IRequestSender<dynamic> requestSender)
        {
            _applicationConfiguration = applicationConfiguration;
            _requestSender = requestSender;
        }

        public async Task<dynamic> SendRequestAsync(string requestUri)
        {
            requestUri = $"{ requestUri}&appid={_applicationConfiguration.APIKey}";
            return await _requestSender.SendGetAsync(_applicationConfiguration.ServiceUrl, requestUri);
        }

    }
}
