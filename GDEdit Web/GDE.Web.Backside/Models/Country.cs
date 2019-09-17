using System.Collections.Generic;
using System.Drawing;

namespace GDE.Web.Backside.Models
{
    public class Country
    {
        /// <summary>Name of the <see cref="Country"/>.</summary>
        public string Name { get; set; }

        /// <summary>Acronym for the <see cref="Country"/>.</summary>
        public string Acronym { get; set; }

        /// <summary><see cref="Country"/>'s Play Count.</summary>
        public int PlayCount { get; set; }
        
        /// <summary><see cref="Country"/>'s Rank.</summary>
        public int Rank { get; set; }

        /// <summary><see cref="Country"/>'s List of Players.</summary>
        public List<User> Players { get; set; }

        /// <summary><see cref="Country"/>'s List of Creators.</summary>
        public List<Creator> Creators { get; set; }

        /// <summary><see cref="Country"/>'s GD Stats Calculated from the Players.</summary>
        public GDStats Stats { get; set; }

        /// <summary>Gets the appropriate image for the <see cref="Country"/>.</summary>
        public Bitmap GetAppropriateImage()
        {
            //TODO: ^
            return null;
        }
    }
}