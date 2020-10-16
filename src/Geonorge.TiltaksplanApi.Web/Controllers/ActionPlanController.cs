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
    [Route("[controller]")]
    public class ActionPlanController : BaseController
    {
        private readonly IActionPlanQuery _actionPlanQuery;
        private readonly IActionPlanService _actionPlanService;

        public ActionPlanController(
            IActionPlanQuery actionPlanQuery,
            IActionPlanService actionPlanService,
            ILogger<ActionPlanController> logger) : base(logger)
        {
            _actionPlanQuery = actionPlanQuery;
            _actionPlanService = actionPlanService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var viewModels = await _actionPlanQuery.GetByIdAsync(id);

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var viewModels = await _actionPlanQuery.GetAllAsync();

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActionPlanViewModel viewModel)
        {
            if (viewModel == null || viewModel.Id != 0)
                return BadRequest();

            try
            {
                var resultViewModel = await _actionPlanService.CreateAsync(viewModel);

                return Created("", resultViewModel);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActionPlanViewModel viewModel)
        {
            if (id == 0 || viewModel == null)
                return BadRequest();

            try
            {
                var resultViewModel = await _actionPlanService.UpdateAsync(id, viewModel);

                return Ok(resultViewModel);
            }
            catch (Exception exception)
            {
                var result = HandleException(exception);

                if (result != null)
                    return result;

                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _actionPlanService.DeleteAsync(id);

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
