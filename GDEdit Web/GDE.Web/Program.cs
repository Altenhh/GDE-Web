using System;
using System.Diagnostics;
using GDE.Web.Data.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GDE.Web
{
    public class Program
    {
        public static Process DatabaseProcess;
        public static QueryFixture Data;
        
        public static void Main(string[] args)
        {
            InitializeDatabase(args);
            CreateHostBuilder(args).Build().Run();

            var currentProcess = Process.GetCurrentProcess();

            currentProcess.Exited += (sender, eventArgs) =>
            {
                DatabaseProcess.Dispose();
                Data.Dispose();
            };
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        public static void InitializeDatabase(string[] args)
        {
            string pathToRethinkDb = null;

            if (args.Length == 0)
            {
                Console.Write("Please input the path to your RethinkDb Directory: ");
                pathToRethinkDb = Console.ReadLine();
            }
            else
                pathToRethinkDb = args[0];

            DatabaseProcess = Process.Start(new ProcessStartInfo
            {
                FileName         = "cmd.exe",
                WorkingDirectory = pathToRethinkDb ?? throw new NullReferenceException("pathToRethinkDB was null."),
                Arguments        = "/c rethinkdb",
            });
            
            Data = new QueryFixture();
            AppDatabase.Database = Data;
        }
    }
}