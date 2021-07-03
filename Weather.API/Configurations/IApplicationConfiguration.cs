using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.API.Configurations
{
    /// <summary>
    /// Abstraction to hide real source of configuration
    /// </summary>
    public interface IApplicationConfiguration
    {
        string ServiceUrl { get; set; }
        string APIKey { get; set; }
    }
}
