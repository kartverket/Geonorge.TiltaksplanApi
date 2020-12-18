using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geonorge.TiltaksplanApi.Application.Models;
using Geonorge.TiltaksplanApi.Controllers;
using Geonorge.TiltaksplanApi.Web.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geonorge.TiltaksplanApi.Web.Controllers
{
    [Route("api/authzinfo")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public SecurityController(
            ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet()]
        public UserViewModel GetResult()
        {
            try
            {
                UserViewModel userInfo = _securityService.GetUserInfo(HttpContext);

                return userInfo;
            }
            catch (Exception exception)
            {
            }

            return new UserViewModel();
        }
    }
}
