using Geonorge.TiltaksplanApi.Domain.Models;
using Geonorge.TiltaksplanApi.Domain.Services.Validation.Rules;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Domain.Services.Validation
{
    public class ActionPlanValidationService : IValidationService<ActionPlan>
    {
        public bool Validate(ActionPlan actionPlan)
        {
            actionPlan.MustHaveVolume();
            actionPlan.MustHaveStatus();
            actionPlan.MustHaveTrafficLight();

            return !actionPlan.ValidationErrors.Any();
        }
    }
}
