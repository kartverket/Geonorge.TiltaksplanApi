using System;
using System.Collections.Generic;
using System.Text;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class UserViewModel
    {
        public string OrganizationName { get; set; }
        public string OrganizationNumber { get; set; }
        public List<string> Roles { get; set; }
    }
}
