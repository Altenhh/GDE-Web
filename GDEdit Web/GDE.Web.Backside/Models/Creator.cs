using System;
using System.Collections.Generic;
using GDAPI.Utilities.Objects.GeometryDash;

namespace GDE.Web.Backside.Models
{
    public class Creator : User
    {
        /// <summary>User created levels.</summary>
        public List<Level> CreatedLevels { get; set; }

        /// <summary>Participated Collabs</summary>
        public List<Collab> ParticipadtedCollabs { get; set; }
        
        //public List<Contests> ParticipadtedContests { get; set; }

        public void CreateCollab(string title, string shortDesc, int maxMembers = 10)
        {
            ParticipadtedCollabs.Add(new Collab(title, shortDesc, maxMembers));
            
            var medal = new Medal("First Collaboration", "Create your first collab!", DateTime.Now, User.Medal.MedalType.Collaboration);
            
            // Gives a new medal for their first collaboration!
            if (!Medals.Exists(m => m == medal))
                GiveMedal(medal);
        }

        /// <summary>Creates a new Creator.</summary>
        /// <param name="username">Username of the <see cref="Creator"/>.</param>
        /// <param name="password">Password for the <see cref="Creator"/>.</param>
        /// <param name="email">Email for the <see cref="Creator"/>.</param>
        public Creator(string username, string password, string email) : base(username, password, email) { }
    }
}