using System.Collections.Generic;
using GDE.Web.Data;
using GDE.Web.Data.Projects;
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

                return table.RunResultAsync<List<ProjectData>>(Database.Connection).Result;
            }
        }
        
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.community;
        }
    }
}