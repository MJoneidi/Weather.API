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
