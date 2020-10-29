using Geonorge.TiltaksplanApi.Application;
using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetupController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUrlProvider _urlProvider;

        public SetupController(
            IWebHostEnvironment webHostEnvironment,
            IUrlProvider urlProvider,
            ILogger<SetupController> logger) : base(logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _urlProvider = urlProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var viewModel = new SetupViewModel
                {
                    Environment = _webHostEnvironment.EnvironmentName,
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
