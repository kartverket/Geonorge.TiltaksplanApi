using System;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Domain.Extensions;
using Geonorge.TiltaksplanApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsController : BaseController
    {
        public OptionsController(
            ILogger<ConfigController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var viewModel = new OptionsViewModel
                {
                    PlanStatuses = EnumExtensions.EnumToSelectOptions<PlanStatus>(),
                    TrafficLights = EnumExtensions.EnumToSelectOptions<TrafficLight>(),
                    MeasureVolume = Constants.MeasureVolume.ToSelectOptions()
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
