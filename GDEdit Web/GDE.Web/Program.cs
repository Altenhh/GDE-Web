using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GDE.Web.Data.Projects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GDE.Web
{
    public class Program
    {
        private const string resourcesFolder = @"bin\Debug\netcoreapp3.1\Resources";
        private const string projectsFile = "projects.json";
        
        public static void Main(string[] args)
        {
            if (!Directory.Exists(resourcesFolder))
                Directory.CreateDirectory(resourcesFolder);

            if (!File.Exists(resourcesFolder + "/" + projectsFile))
            { var    data = new List<ProjectData>
                {
                    new ProjectData()
                };
                
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(resourcesFolder + "/" + projectsFile, json);

                Console.WriteLine($@"File: {projectsFile} has been created in {Directory.GetCurrentDirectory()}\{resourcesFolder}, Please go and edit that file to include any projects (NOT REQUIRED)");
            }
            else
            {
                List<ProjectData> data =
                    JsonConvert.DeserializeObject<List<ProjectData>>(
                        File.ReadAllText(resourcesFolder + "/" + projectsFile));

                TempDatabase.ProjectDatas = data;
            }
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}