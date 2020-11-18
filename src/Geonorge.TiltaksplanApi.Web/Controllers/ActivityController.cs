using System;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services;
using Geonorge.TiltaksplanApi.Web.Configuration;
using Geonorge.TiltaksplanApi.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("{id:int}/{culture?}")]
        public async Task<IActionResult> GetById(int id, string culture = "nb-NO")
        {
            try
            {
                var viewModels = await _activityQuery.GetByIdAsync(id, culture);

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

        [HttpGet("{culture?}")]
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

        //[AuthorizeGeoID]
        [HttpPost]
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

        //[AuthorizeGeoID]
        [HttpPut("{id}")]
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

        //[AuthorizeGeoID]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _activityService.DeleteAsync(id);

                return new NoContentResult();
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
