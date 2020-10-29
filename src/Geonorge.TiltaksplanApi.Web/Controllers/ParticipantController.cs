using System;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : BaseController
    {
        private const int CacheDurationSeconds = 2592000;
        private readonly IParticipantService _participantService;

        public ParticipantController(
            IParticipantService participantService,
            ILogger<ParticipantController> logger) : base(logger)
        {
            _participantService = participantService;
        }

        [ResponseCache(Duration = CacheDurationSeconds, Location = ResponseCacheLocation.Any, NoStore = false)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var participants = await _participantService.GetAllAsync();

                return Ok(participants);
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
