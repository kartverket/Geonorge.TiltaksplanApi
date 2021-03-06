﻿using System;
using Geonorge.TiltaksplanApi.Application.Exceptions;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private readonly ILogger<ControllerBase> _logger;

        protected BaseController(
            ILogger<ControllerBase> logger)
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
                case AuthorizationException ex:
                    return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
                case WorkProcessException ex:
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
                case Exception _:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return null;
        }

        protected void LogValidationErrors(ViewModelWithValidation viewModel)
        {
            _logger.LogInformation($"Error validating {viewModel.GetType().Name}: {string.Join(", ", viewModel.ValidationErrors)}");
        }
    }
}
