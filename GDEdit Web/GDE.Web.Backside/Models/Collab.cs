using System;

namespace GDE.Web.Backside.Models
{
    public class Collab
    {
        public string Title { get; set; }

        public int MaxMembers { get; set; }
            
        public Creator[] Hosts { get; set; }
            
        public Creator[] Collaborators { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public float PlannedDifficulty { get; set; }

        public TimeSpan PlannedLength { get; set; }

        public string DiscordLink { get; set; }

        public Collab(string title, string shortDesc, int maxMembers = 10)
        {
            Title = title;
            ShortDescription = shortDesc;
            MaxMembers = maxMembers;
        }
        
        public Collab(string title, string shortDesc, Creator[] hosts, int maxMembers = 10)
        {
            Title = title;
            ShortDescription = shortDesc;
            MaxMembers = maxMembers;
            Hosts = hosts;
        }
    }
}