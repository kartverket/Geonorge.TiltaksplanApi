using Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID;
using System.Collections.Generic;

namespace Geonorge.TiltaksplanApi.Application.Models
{
    public class UserViewModel
    {
        public string OrganizationName { get; set; }
        public long OrganizationNumber { get; set; }
        public List<string> Roles { get; set; }

        public bool IsAdmin => Roles?.Contains(GeonorgeRole.Admin) ?? false;
        public bool HasRole(string role) => Roles?.Contains(role) ?? false;
    }
}
