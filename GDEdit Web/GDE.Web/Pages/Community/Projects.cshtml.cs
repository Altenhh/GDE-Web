using System.Collections.Generic;
using GDE.Web.Data;
using GDE.Web.Data.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDE.Web.Pages.Community
{
    public class ProjectsModel : PageModel
    {
        public List<ProjectData> Data { get; set; } = new List<ProjectData>();
        
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.community;
        }
    }
}