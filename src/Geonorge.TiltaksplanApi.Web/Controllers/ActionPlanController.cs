using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionPlanController : ControllerBase
    {
        private readonly IActionPlanService _actionPlanService;
        private readonly ILogger<ActionPlanController> _logger;

        public ActionPlanController(
            IActionPlanService actionPlanService,
            ILogger<ActionPlanController> logger)
        {
            _actionPlanService = actionPlanService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var viewModels = await _actionPlanService
                .GetAllAsync();

            return Ok(viewModels);
        }
    }
}
