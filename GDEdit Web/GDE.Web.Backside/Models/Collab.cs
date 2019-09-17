namespace GDE.Web.Backside.Models
{
    public class Collab
    {
        public OverviewData OverviewView { get; set; }
        
        /// <summary>All the metadata for the collab</summary>
        public class OverviewData
        {
            public string Title { get; set; }
            
            //Can be multiple hosts so im going to put it into an array
            public Creator[] Hosts { get; set; }
        }
        
        /*public IssuesData IssuesView { get; set; }
        public PullRequestsData PullRequestsView { get; set; }
        public NotesData NotesView { get; set; }
        public AboutData AboutView { get; set; }*/
    }
}