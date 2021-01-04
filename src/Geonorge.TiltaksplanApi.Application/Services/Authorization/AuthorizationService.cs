using Geonorge.TiltaksplanApi.Application.Queries;
using Geonorge.TiltaksplanApi.Application.Services.Authorization.GeoID;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geonorge.TiltaksplanApi.Application.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly List<ActivityAccess> _accesses;
        private readonly IGeoIDService _geoIDService;
        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(
            IGeoIDService geoIDService,
            IMeasureQuery measureQuery,
            ILogger<AuthorizationService> logger)
        {
            _geoIDService = geoIDService;
            _logger = logger;

            _accesses = new List<ActivityAccess>
            {
                new ActivityAccess(UserActivity.CreateMeasure, new List<string> { GeonorgeRole.Admin }),
                new ActivityAccess(UserActivity.UpdateMeasure, new List<string> { GeonorgeRole.Admin }),
                new ActivityAccess(UserActivity.DeleteMeasure, new List<string> { GeonorgeRole.Admin }),
                new ActivityAccess(UserActivity.CreateActivity, new List<string> { GeonorgeRole.Admin, GeonorgeRole.Editor }, measureQuery.HasOwnership),
                new ActivityAccess(UserActivity.UpdateActivity, new List<string> { GeonorgeRole.Admin, GeonorgeRole.Editor }, measureQuery.HasOwnership),
                new ActivityAccess(UserActivity.DeleteActivity, new List<string> { GeonorgeRole.Admin, GeonorgeRole.Editor }, measureQuery.HasOwnership),
            };
        }

        public async Task AuthorizeActivity(UserActivity activity, int? entityId)
        {
            var access = _accesses.SingleOrDefault(access => access.Activity == activity);

            if (access == null)
            {
                _logger.LogWarning($"No access defined for the activity '{activity}'.");
                throw new AuthorizationException($"No access defined for the activity '{activity}'.");
            }

            var user = await _geoIDService.GetUser();

            if (user == null)
            {
                _logger.LogWarning($"No authenticated user found.");
                throw new AuthorizationException($"No authenticated user found.");
            }

            if (user.IsAdmin)
                return;

            var hasRole = access.Roles.Any(role => user.HasRole(role));

            if (!hasRole)
            {
                _logger.LogWarning($"The user doesn't have a role with access to the activity '{activity}'.");
                throw new AuthorizationException($"The user doesn't have a role with access to the activity '{activity}'.");
            }

            if (access.OwnershipAction == null)
                return;

            var hasOwnership = await access.OwnershipAction(entityId.GetValueOrDefault(), user.OrganizationNumber);

            if (hasOwnership)
                return;

            _logger.LogWarning($"The user doesn't have the ownership which grants access to the activity '{activity}'.");
            throw new AuthorizationException($"The user doesn't have the ownership which grants access to the activity '{activity}'.");
        }
    }
}
