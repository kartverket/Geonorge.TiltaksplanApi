using Geonorge.TiltaksplanApi.Application;
using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUrlProvider _urlProvider;

        public ConfigController(
            IWebHostEnvironment webHostEnvironment,
            IUrlProvider urlProvider,
            ILogger<ConfigController> logger) : base(logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _urlProvider = urlProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var viewModel = new ConfigViewModel
                {
                    ApiUrls = _urlProvider.ApiUrls()
                };

                return Ok(viewModel);
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
