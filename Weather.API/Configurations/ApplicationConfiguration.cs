using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.API.Configurations
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public ApplicationConfiguration(IConfiguration configuration)
        {
            this.ServiceUrl = configuration["Service:Url"];
        }

        public string ServiceUrl { get; set; }
    }
}
