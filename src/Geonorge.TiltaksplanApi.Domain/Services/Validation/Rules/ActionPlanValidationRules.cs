using Geonorge.TiltaksplanApi.Domain.Models;
using System.Reflection;

namespace Geonorge.TiltaksplanApi.Domain.Services.Validation.Rules
{
    internal static class ActionPlanValidationRules
    {
        public static bool MustHaveVolume(this ActionPlan actionPlan)
        {
            if (actionPlan.Volume != 0)
                return true;

            actionPlan.AddValidationError(nameof(actionPlan.Volume), $"{typeof(ActionPlan).Name}_{MethodBase.GetCurrentMethod().Name}");
            return false;
        }

        public static bool MustHaveStatus(this ActionPlan actionPlan)
        {
            if (actionPlan.Status >= 1 && actionPlan.Status <= 5)
                return true;

            actionPlan.AddValidationError(nameof(actionPlan.Status), $"{typeof(ActionPlan).Name}_{MethodBase.GetCurrentMethod().Name}");
            return false;
        }

        public static bool MustHaveTrafficLight(this ActionPlan actionPlan)
        {
            var trafficLight = (int)actionPlan.TrafficLight;

            if (trafficLight >= 1 && trafficLight <= 3)
                return true;

            actionPlan.AddValidationError(nameof(actionPlan.TrafficLight), $"{typeof(ActionPlan).Name}_{MethodBase.GetCurrentMethod().Name}");
            return false;
        }
    }
}
