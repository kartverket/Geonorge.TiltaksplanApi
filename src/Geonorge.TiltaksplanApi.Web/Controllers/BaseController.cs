using System;
using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger<ControllerBase> _logger;

        protected BaseController(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleException(Exception exception)
        {
            _logger.LogError(exception.ToString());

            switch (exception)
            {
                case ArgumentException _:
                case FormatException _:
                    return BadRequest();
                case Exception _:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return null;
        }

        protected void LogValidationErrors(ViewModelWithValidation viewModel)
        {
            _logger.LogInformation($"Error validating {viewModel.GetType().Name}: {viewModel.AllErrorCodes()}");
        }
    }
}
