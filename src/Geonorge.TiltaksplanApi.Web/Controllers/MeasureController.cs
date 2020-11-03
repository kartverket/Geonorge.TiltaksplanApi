﻿using System;
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

        [HttpGet("{id:int}/{culture?}")]
        public async Task<IActionResult> GetById(int id, string culture = "nb-NO")
        {
            try
            {
                var viewModels = await _measureQuery.GetByIdAsync(id, culture);

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
                var viewModels = await _measureQuery.GetAllAsync(culture);

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
        public async Task<IActionResult> Create([FromBody] MeasureViewModel viewModel)
        {
            if (viewModel == null || viewModel.Id != 0)
                return BadRequest();

            try
            {
                var resultViewModel = await _measureService.CreateAsync(viewModel);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MeasureViewModel viewModel)
        {
            if (id == 0 || viewModel == null)
                return BadRequest();

            try
            {
                var resultViewModel = await _measureService.UpdateAsync(id, viewModel);

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            try
            {
                await _measureService.DeleteAsync(id);

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
