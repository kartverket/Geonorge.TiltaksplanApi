using System;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [Route("Authzinfo")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IGeoIDService _geoIDService;

        public UserController(
            IGeoIDService geoIDService,
            ILogger<UserController> logger) : base(logger)
        {
            _geoIDService = geoIDService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _geoIDService.GetUser();

                if (user == null)
                    return NotFound();

                return Ok(user);
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
