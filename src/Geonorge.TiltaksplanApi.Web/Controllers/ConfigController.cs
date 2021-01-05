using Geonorge.TiltaksplanApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : BaseController
    {
        private readonly IConfigService _configService;

        public ConfigController(
            IConfigService configService,
            ILogger<ConfigController> logger) : base(logger)
        {
            _configService = configService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var config = _configService.Get();

                return Ok(config);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }
    }
}
