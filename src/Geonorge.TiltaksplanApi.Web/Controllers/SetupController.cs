using Geonorge.TiltaksplanApi.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetupController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUrlProvider _urlProvider;

        public SetupController(
            IWebHostEnvironment webHostEnvironment,
            IUrlProvider urlProvider)
        {
            _webHostEnvironment = webHostEnvironment;
            _urlProvider = urlProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var viewModel = _urlProvider.ApiUrls();

            return Ok(viewModel);
        }
    }
}
