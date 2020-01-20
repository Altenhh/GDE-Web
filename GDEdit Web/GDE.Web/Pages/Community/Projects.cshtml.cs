using System.Collections.Generic;
using System.Threading.Tasks;
using GDE.Web.Data;
using GDE.Web.Data.Projects;
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
                List<ProjectData> result = null;

                new Task(() =>
                {
                    result = table.Map(p => new
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
                    }).RunResult<List<ProjectData>>(Database.Connection);
                }).Wait();

                return result;
            }
        }
        
        public void OnGet()
        {
            GlobalVariables.CurrentSection = LinkItems.community;
        }
    }
}