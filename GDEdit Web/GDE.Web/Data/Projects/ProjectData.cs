namespace GDE.Web.Data.Projects
{
    public class ProjectData : ProjectDataMini
    {
        /// <summary>How many issues the project has.</summary>
        public int Issues { get; set; }

        /// <summary>How many Pull Requests the project has (<seealso cref="https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/about-pull-requests"/>).</summary>
        public int PullRequests { get; set; }

        /// <summary>How many forks the project has.</summary>
        public int Forks { get; set; }

        /// <summary>What License the project is using</summary>
        public string License { get; set; }
    }
}