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
    public class MeasureController : BaseController
    {
        private readonly IMeasureQuery _measureQuery;
        private readonly IMeasureService _measureService;

        public MeasureController(
            IMeasureQuery measureQuery,
            IMeasureService measureService,
            ILogger<MeasureController> logger) : base(logger)
        {
            _measureQuery = measureQuery;
            _measureService = measureService;
        }

        [HttpGet("Measure/{culture ?}")]
        public async Task<IActionResult> GetAll(string culture = "nb-NO", string organization = "")
        {
            try
            {
                var viewModels = await _measureQuery.GetAllAsync(culture, organization);

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

        [HttpGet("Measure/{number:int}/{culture?}")]
        public async Task<IActionResult> GetByNumber(int number, string culture = "nb-NO")
        {
            try
            {
                var viewModel = await _measureQuery.GetByNumberAsync(number, culture);

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

        [HttpPost("Measure")]
        public async Task<IActionResult> Create([FromBody] MeasureViewModel viewModel)
        {
            if (viewModel == null || viewModel.Id != 0)
                return BadRequest();

            try
            {
                var resultViewModel = await _measureService.CreateAsync(viewModel);

                if (resultViewModel?.IsValid ?? false)
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

        [HttpPut("Measure/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MeasureViewModel viewModel)
        {
            if (id == 0 || viewModel == null)
                return BadRequest();

            try
            {
                var resultViewModel = await _measureService.UpdateAsync(id, viewModel);

                if (resultViewModel?.IsValid ?? false)
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

        [HttpDelete("Measure/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _measureService.DeleteAsync(id);

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
