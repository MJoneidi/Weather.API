using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Weather.API.Models.Exceptions;

namespace Weather.API.Infrastructure
{
    public class HttpRequestSender<T> : IRequestSender<T> where T : new()
    {
        public async Task<dynamic> SendPostAsync(string url, string queryString, string jsonPayload)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(url) };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(queryString, content);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result = await response.Content.ReadAsStringAsync();

                client.Dispose();

                return Json.Decode(result);               
            }
            else
            {
                throw new HttpSenderException(response.StatusCode.ToString(), response.ReasonPhrase);
            }
        }

        public async Task<dynamic> SendGetAsync(string url, string queryString)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(url) };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var result = await response.Content.ReadAsStringAsync();

                client.Dispose();

                return Json.Decode(result);                
            }
            else
            {
                throw new HttpSenderException(response.StatusCode.ToString(), response.ReasonPhrase);
            }
        }
    }
}
