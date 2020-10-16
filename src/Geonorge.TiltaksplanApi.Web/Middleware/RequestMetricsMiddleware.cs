using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Middleware
{
    public class RequestMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMetricsMiddleware> _logger;

        public RequestMetricsMiddleware(RequestDelegate next, ILogger<RequestMetricsMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public RequestMetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            await _next(context);

            _logger.LogInformation("{@metrics:l}", new RequestMetrics
            {
                User = "Username", //context.User.Identity.Name,
                Method = context.Request.Method,
                Path = context.Request.Path,
                ResponseCode = context.Response.StatusCode,
                ProcessingTime = watch.ElapsedMilliseconds
            });
        }
    }
}
