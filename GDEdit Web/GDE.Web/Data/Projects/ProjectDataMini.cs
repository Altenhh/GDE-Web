using System;

namespace GDE.Web.Data.Projects
{
    public class ProjectDataMini
    {
        public string Name { get; set; }

        public string BackgroundURL { get; set; }

        public string LogoURL { get; set; }

        public string MainLanguage { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public int Stars { get; set; }
    }
}