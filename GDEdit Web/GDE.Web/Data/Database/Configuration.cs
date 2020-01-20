using RethinkDb.Driver;
using static System.Configuration.ConfigurationManager;

namespace GDE.Web.Data.Database
{
    public static class Configuration
    {
        public static string ServerName
        {
            get => AppSettings["serverName"];
            set => AppSettings["serverName"] = value;
        }
        
        public static int ServerPort
        {
            get
            {
                string port = AppSettings["ServerPort"];
                return port == null ? RethinkDBConstants.DefaultPort : int.Parse(port);
            }
            set => AppSettings["ServerPort"] = value.ToString();
        }
    }
}