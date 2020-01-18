namespace GDE.Web.Data.Projects
{
    public class ProjectData : ProjectDataMini
    {
        public int Issues { get; set; }

        public int PullRequests { get; set; }

        public int Forks { get; set; }

        public string License { get; set; }
    }
}