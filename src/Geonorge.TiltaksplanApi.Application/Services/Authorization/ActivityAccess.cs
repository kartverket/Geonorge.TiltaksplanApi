using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services.Authorization
{
    public class ActivityAccess
    {
        public UserActivity Activity { get; }
        public List<string> Roles { get; }
        public Func<int, long, Task<bool>> OwnershipAction { get; }

        public ActivityAccess(
            UserActivity activity,
            List<string> roles,
            Func<int, long, Task<bool>> ownershipAction = null)
        {
            Activity = activity;
            Roles = roles;
            OwnershipAction = ownershipAction;
        }
    }
}
