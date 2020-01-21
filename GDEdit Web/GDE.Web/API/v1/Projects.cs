using System.Collections.Generic;
using GDE.Web.Entities.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RethinkDb.Driver.Ast;
using static GDE.Web.Services.Database.AppDatabase;

namespace GDE.Web.API.v1
{
    [Route("api/v1/[controller]"), ApiController, Authorize]
    public class Projects : ControllerBase
    {
        private Table projects
        {
            get
            {
                var table = DatabaseService.Database.Table("Projects");

                table.Map(p => new
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
                });

                return table;
            }
        }
            
        [HttpGet]
        public IEnumerable<ProjectData> Get()
        {
            return projects.RunResultAsync<List<ProjectData>>(DatabaseService.Connection).Result.ToArray();
        }

        [HttpGet("{id}")]
        public ProjectData Get(string id)
        {
            return projects.Get(id).RunResultAsync<ProjectData>(DatabaseService.Connection).Result;
        }
    }
}