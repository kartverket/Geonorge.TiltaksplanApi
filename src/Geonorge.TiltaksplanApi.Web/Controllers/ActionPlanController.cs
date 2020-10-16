using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionPlanController : BaseController
    {
        private readonly IAsyncQuery<IEnumerable<ActionPlanViewModel>> _actionPlanQuery;

        public ActionPlanController(
            IAsyncQuery<IEnumerable<ActionPlanViewModel>> actionPlanQuery,
            ILogger<ActionPlanController> logger) : base(logger)
        {
            _actionPlanQuery = actionPlanQuery;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var yo = id;
                var viewModels = await _actionPlanQuery.ExecuteAsync();

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
                var viewModels = await _actionPlanQuery.ExecuteAsync();

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
    }
}
