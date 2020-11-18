namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class MeasureTranslation : ValidatableEntity
    {
        public int MeasureId { get; set; }
        public string Name { get; set; }
        public string Progress { get; set; }
        public string Comment { get; set; }
        public string LanguageCulture { get; set; }
        public Language Language { get; set; }

        public override void Update(EntityBase updatedEntity)
        {
            var updated = (MeasureTranslation) updatedEntity;

            if (Name != updated.Name)
                Name = updated.Name;

            if (Progress != updated.Progress)
                Progress = updated.Progress;

            if (Comment != updated.Comment)
                Comment = updated.Comment;
        }
    }
}
