using System;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("Organizations")]
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationQuery _organizationQuery;

        public OrganizationController(
            IOrganizationQuery organizationQuery,
            ILogger<OrganizationController> logger) : base(logger)
        {
            _organizationQuery = organizationQuery;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var participants = await _organizationQuery.GetAllAsync();

                return Ok(participants);
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
