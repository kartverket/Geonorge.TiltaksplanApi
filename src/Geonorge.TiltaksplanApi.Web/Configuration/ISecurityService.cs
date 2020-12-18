using Geonorge.TiltaksplanApi.Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.TiltaksplanApi.Web.Configuration
{
    public interface ISecurityService
    {
        public UserViewModel GetUserInfo(HttpContext httpContext);
    }
}
