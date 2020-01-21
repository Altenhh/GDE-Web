using System.Collections.Generic;
using GDE.Web.Data;
using GDE.Web.Entities.Projects;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static GDE.Web.Services.Database.AppDatabase;

namespace GDE.Web.Pages.Community
{
    public class ProjectsModel : PageModel
    {
        public List<ProjectData> Data
        {
            get
            {
                var table = DatabaseService.Database.Table("Projects");

                return table.RunResultAsync<List<ProjectData>>(DatabaseService.Connection).Result;
            }
        }
        
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.community;
        }
    }
}