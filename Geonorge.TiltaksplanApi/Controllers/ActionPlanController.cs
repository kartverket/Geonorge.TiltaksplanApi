using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionPlanController : ControllerBase
    {

        private readonly ILogger<ActionPlanController> _logger;

        public ActionPlanController(ILogger<ActionPlanController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ActionPlan> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ActionPlan
            {
                Name = "Tiltak" + rng.Next(-20, 55)
            })
            .ToArray();
        }
    }
}
