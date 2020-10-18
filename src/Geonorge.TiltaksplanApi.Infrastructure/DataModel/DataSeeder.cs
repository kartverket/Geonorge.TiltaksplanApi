using Geonorge.TiltaksplanApi.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Geonorge.TiltaksplanApi.Infrastructure.DataModel
{
    public class DataSeeder
    {
        public static void SeedLanguages(ActionPlanContext context)
        {
            if (context.Languages.Count() == 0)
            {
                var languages = new List<Language>
                {
                    new Language { Culture = "nb-NO", Name = "Norsk", },
                    new Language { Culture = "en-GB", Name = "Engelsk",  }
                };

                context.AddRange(languages);
                context.SaveChanges();
            }
        }
    }
}
