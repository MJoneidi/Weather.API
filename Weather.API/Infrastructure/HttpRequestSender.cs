using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Weather.API.Models.Exceptions;

namespace Weather.API.Infrastructure
{
    public class HttpRequestSender<T> : IRequestSender<T> where T : new()
    {
        private readonly ILogger<HttpRequestSender<T>> _logger;

        public HttpRequestSender(ILogger<HttpRequestSender<T>> logger)
        {
            _logger = logger;
        }
        public async Task<dynamic> SendPostAsync(string url, string queryString, string jsonPayload)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(queryString, content);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var jsonData = await response.Content.ReadAsStringAsync();
                    client.Dispose();
                    return JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
                }
                else
                {
                    throw new HttpSenderException(response.StatusCode.ToString(), response.ReasonPhrase);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<dynamic> SendGetAsync(string url, string queryString)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(queryString);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    client.Dispose();
                    return JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());
                }
                else                
                    throw new HttpSenderException(response.StatusCode.ToString(), response.ReasonPhrase);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
