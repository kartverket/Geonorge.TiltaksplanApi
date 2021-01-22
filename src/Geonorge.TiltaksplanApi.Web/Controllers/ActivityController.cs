using System;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services;
using Geonorge.TiltaksplanApi.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    public class ActivityController : BaseController
    {
        private readonly IActivityQuery _activityQuery;
        private readonly IActivityService _activityService;

        public ActivityController(
            IActivityQuery activityQuery,
            IActivityService activityService,
            ILogger<ActivityController> logger) : base(logger)
        {
            _activityQuery = activityQuery;
            _activityService = activityService;
        }

        [HttpGet("Activity/{culture?}")]
        public async Task<IActionResult> GetAll(string culture = "nb-NO")
        {
            try
            {
                var viewModels = await _activityQuery.GetAllAsync(culture);

                return Ok(viewModels);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpGet("Measure/{measureNumber:int}/Activities/{culture?}")]
        public async Task<IActionResult> GetAllByMeasureNumber(int measureNumber, string culture = "nb-NO")
        {
            try
            {
                var viewModels = await _activityQuery.GetAllByMeasureNumberAsync(measureNumber, culture);

                return Ok(viewModels);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpGet("Measure/{measureNumber:int}/Activity/{number:int}/{culture?}")]
        public async Task<IActionResult> GetByNumber(int measureNumber, int number, string culture = "nb-NO")
        {
            try
            {
                var viewModel = await _activityQuery.GetByNumberAsync(measureNumber, number, culture);

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

        [HttpPost("Activity")]
        public async Task<IActionResult> Create([FromBody] ActivityViewModel viewModel)
        {
            if (viewModel == null || viewModel.Id != 0)
                return BadRequest();

            try
            {
                var resultViewModel = await _activityService.CreateAsync(viewModel);

                if (resultViewModel.IsValid)
                    return Created("", resultViewModel);

                LogValidationErrors(resultViewModel);

                return BadRequest(resultViewModel.ValidationErrors);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpPut("Activity/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActivityViewModel viewModel)
        {
            if (id == 0 || viewModel == null)
                return BadRequest();

            try
            {
                var resultViewModel = await _activityService.UpdateAsync(id, viewModel);

                if (resultViewModel.IsValid)
                    return Ok(resultViewModel);

                LogValidationErrors(resultViewModel);

                return BadRequest(resultViewModel.ValidationErrors);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpDelete("Activity/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _activityService.DeleteAsync(id);

                return Ok(id);
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
