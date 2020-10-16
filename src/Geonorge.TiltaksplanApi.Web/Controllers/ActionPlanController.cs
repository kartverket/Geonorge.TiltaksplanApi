using System.Collections.Generic;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionPlanController : ControllerBase
    {
        private readonly IAsyncQuery<IEnumerable<ActionPlanViewModel>> _actionPlanQuery;
        private readonly ILogger<ActionPlanController> _logger;

        public ActionPlanController(
            IAsyncQuery<IEnumerable<ActionPlanViewModel>> actionPlanQuery,
            ILogger<ActionPlanController> logger)
        {
            _actionPlanQuery = actionPlanQuery;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Hello, {Name}!", "Tor Anders");

            var viewModels = await _actionPlanQuery.ExecuteAsync();

            return Ok(viewModels);
        }
    }
}
