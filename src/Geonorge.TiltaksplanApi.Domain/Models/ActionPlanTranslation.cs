namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class ActionPlanTranslation : ValidatableEntity
    {
        public int ActionPlanId { get; set; }
        public string Name { get; set; }
        public string Progress { get; set; }
        public string Results { get; set; }
        public string Comment { get; set; }
        public string LanguageCulture { get; set; }
        public Language Language { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (ActionPlanTranslation) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (Progress != updated.Progress)
                Progress = updated.Progress;

            if (Results != updated.Results)
                Results = updated.Results;

            if (Comment != updated.Comment)
                Comment = updated.Comment;
        }
    }
}
