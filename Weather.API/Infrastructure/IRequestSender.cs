using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.API.Infrastructure
{
    public interface IRequestSender<T>
    {
        Task<dynamic> SendPostAsync(string url, string requestUri, string jsonString);
        Task<dynamic> SendGetAsync(string url, string queryString);
    }
}
