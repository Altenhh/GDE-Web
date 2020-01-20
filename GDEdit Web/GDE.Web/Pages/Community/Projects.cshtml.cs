﻿using System.Collections.Generic;
using System.Linq;
using GDE.Web.Data;
using GDE.Web.Data.Projects;
using GDE.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static GDE.Web.Data.Database.AppDatabase;

namespace GDE.Web.Pages.Community
{
    public class ProjectsModel : PageModel
    {
        public List<ProjectData> Data
        {
            get
            {
                var table = Database.Database.Table("Projects");

                return table.Map(p => new
                {
                    Authors         = p["Authors"],
                    BackgroundURL   = p["BackgroundURL"],
                    Description     = p["Description"],
                    Discord         = p["Discord"],
                    Forks           = p["Forks"],
                    Issues          = p["Issues"],
                    LanguageAcronym = p["LanguageAcronym"],
                    License         = p["License"],
                    LogoURL         = p["LogoURL"],
                    MainLanguage    = p["MainLanguage"],
                    Name            = p["Name"],
                    PullRequests    = p["PullRequests"],
                    Stars           = p["Stars"],
                    ID              = p["id"],
                    LastUpdated     = p["LastUpdated"],
                    Site            = p["Site"],
                    Github          = p["Github"]
                }).RunResultAsync<List<ProjectData>>(Database.Connection).Result;;
            }
        }
        
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.community;
        }
    }
}