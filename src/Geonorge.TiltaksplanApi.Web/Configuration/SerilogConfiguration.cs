using Geonorge.TiltaksplanApi.Web.Middleware;
using Microsoft.Extensions.Configuration;
using Serilog;
using Newtonsoft.Json;

namespace Blt.Web.Configuration
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(IConfigurationRoot configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Destructure.ByTransforming<RequestMetrics>(JsonConvert.SerializeObject)
                .CreateLogger();
        }
    }
}