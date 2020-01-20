using System;
using System.Collections.Generic;

namespace GDE.Web.Data.Projects
{
    public class ProjectDataMini
    {
        //TODO: guid?
        public int ID { get; set; }

        public string Description { get; set; }

        public string Site { get; set; }
        
        public string Github { get; set; }
        
        /// <summary>Name of the project.</summary>
        public string Name { get; set; }
        
        /// <summary>List of owners that are handling the project.</summary>
        public List<string> Authors { get; set; }

        /// <summary>Background URL of the project (OPTIONAL).</summary>
        public string BackgroundURL { get; set; }

        /// <summary>Logo URL of the project.</summary>
        public string LogoURL { get; set; }

        /// <summary>What language the project was written in.</summary>
        public string MainLanguage { get; set; }

        /// <summary>Acronym of the language written in (i.e c# -> cs, visual basic -> vb).</summary>
        public string LanguageAcronym { get; set; }

        /// <summary>Last time the project has been updated.</summary>
        /// <returns><see cref="DateTime.Now"/></returns>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>How many stars the project currently has.</summary>
        public int Stars { get; set; }
    }
}