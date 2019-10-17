using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GDAPI.Utilities.Objects.GeometryDash;
using JetBrains.Annotations;
using osu.Framework.MathUtils;

namespace GDE.Web.Backside.Models
{
    /// <summary>Basic information on the lowest level possible for every <see cref="User"/>.</summary>
    public class User
    {
        // Properties //
        /// <summary>The Username of the <see cref="User"/>.</summary>
        public List<string> Username { get; set; }
        
        /// <summary>A lowercased version of Username</summary>
        public List<string> UsernameClean { get; set; }

        // Will be parsed into Markdown
        /// <summary>A description of the <see cref="User"/>, can be placed as a Markdown element.</summary>
        public string AboutMe { get; set; }

        /// <summary>The ID of the <see cref="User"/>.</summary>
        public long ID { get; set; }

        /// <summary>The rank of the <see cref="User"/>.</summary>
        public Rank UserRank { get; set; }

        public class Rank
        {
            /// <summary>Globalized rank number.</summary>
            public int Global { get; set; }
            
            /// <summary>Regional rank number.</summary>
            public int Regional { get; set; }
        }
        
        /// <summary>Label for the <see cref="User"/>.</summary>
        public Labels UserLabel { get; set; }
        
        public enum Labels
        {
            Owner,
            Developer,
            Contributor,
            Admin,
            Mod,
            Important,
            Normal = default
        }
        
        /// <summary>What Play Style does the <see cref="User"/> do?</summary>
        public Styles PlayStyle{ get; set; }

        public enum Styles
        {
            Keyboard,
            Mouse,
            Controller
        }
        
        // Stats //
        /// <summary>Tracks how many followers the <see cref="User"/> has.</summary>
        public List<User> Followers { get; set; }

        /// <summary>Current level.</summary>
        public int Level { get; set; }
        
        /// <summary>Current Experience points.</summary>
        public float XP { get; set; }
        
        /// <summary>How many Experience are required till the next Level.</summary>
        public float XpToNextLevel { get; set; }

        /// <summary>Geometry Dash Stats.</summary>
        public GDStats Stats { get; set; }
        
        // Profile links //

        public string CurrentLocation { get; set; }

        public string Interests { get; set; }

        public string Occupation { get; set; }

        ///// <summary>A more dynamic approach to connecting Github to the Changelog.</summary>
        //public string GithubUrl { get; set; }

        /// <summary>Is the <see cref="User"/> a Supporter?</summary>
        public bool Supporter { get; set; }

        /// <summary>Should the <see cref="User"/> Appear as online?</summary>
        public bool ViewOnline { get; set; }
        
        // Notifications
        /// <summary>Notify the <see cref="User"/> of anything.</summary>
        public bool Notify { get; set; }
        
        // Country stuffs
        /// <summary>Timezone the <see cref="User"/> is currently living in.</summary>
        public DateTimeOffset Timezone { get; set; }

        /// <summary>Country the <see cref="User"/> is currently living in.</summary>
        public Country Country { get; set; }
        
        // Others
        /// <summary>The <see cref="User"/>'s Favorite Levels.</summary>
        public List<Level> FavoriteLevels { get; set; }
        
        /// <summary>Badges the <see cref="User"/> has received over the years.</summary>
        public List<Medal> Medals { get; set; }
        
        public class Medal
        {
            /// <summary>Image URL for the <see cref="Medal"/>.</summary>
            public string Image { get; set; }

            /// <summary>Title of the <see cref="Medal"/>.</summary>
            public string Title { get; set; }

            public string ShortDescription { get; set; }

            public DateTime AchievedOn { get; set; }

            public MedalType Type { get; set; }
            
            public enum MedalType
            {
                Collaboration,
                Skill,
                Level,
            }

            public bool Equals(Medal other)
            {
                return Title == other.Title && ShortDescription == other.ShortDescription;
            }

            public override bool Equals(object obj)
            {
                return obj is Medal other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Title.GetHashCode() * 397) ^ ShortDescription.GetHashCode();
                }
            }

            public static bool operator ==(Medal left, Medal right) => left.Equals(right);
            public static bool operator !=(Medal left, Medal right) => !left.Equals(right);

            public Medal(string title, string shortDescription, DateTime achievedOn, MedalType type, string image = null)
            {
                Image = image;
                Title = title;
                ShortDescription = shortDescription;
                AchievedOn = achievedOn;
                Type = type;
            }
        }

        /// <summary>Social Medias.</summary>
        public Socials SocialMedias { get; set; }

        public class Socials
        {
            /// <summary>Twitter Username.</summary>
            public string TwitterUsername { get; set; }
            /// <summary>Twitter Link.</summary>
            public string TwitterUrl { get; set; }
            
            /// <summary>YouTube Username.</summary>
            public string YoutubeUsername { get; set; }
            /// <summary>YouTube Link.</summary>
            public string YoutubeUrl { get; set; }
            
            /// <summary>Twitch Username.</summary>
            public string TwitchUsername { get; set; }
            /// <summary>Twitch Link.</summary>
            public string TwitchUrl { get; set; }
            
            /// <summary>Website Link.</summary>
            public string WebsiteUrl { get; set; }
            
            /// <summary>Discord Profile/</summary>
            public string DiscordTag { get; set; }
        }

        /// <summary>Token used for Encryption.</summary>
        public string Token { get; set; }
        
        /// <summary>Dates where events has occured.</summary>
        public DatesData Dates { get; set; }

        /// <summary>The <see cref="User"/>'s Email (salted).</summary>
        public string Email { get; set; }
        /// <summary>The <see cref="User"/>'s Password (salted).</summary>
        public string Password { get; set; }

        public class DatesData
        {
            /// <summary>The date where the <see cref="User"/> was registered.</summary>
            public DateTime DateRegistered { get; set; }
            /// <summary>Last time the <see cref="User"/> has visited the Site.</summary>
            public DateTime LastVisit { get; set; }
            /// <summary>Last post the <see cref="User"/> has posted on the Site.</summary>
            public DateTime LastPost { get; set; }
        }
        
        // Constructor //
        public User(string username, string password, string email)
        {
            ChangeUsername(username, username.ToLower());
            Password = password;
            Email = email;

            // Initializers //
            Dates = new DatesData();
            SocialMedias = new Socials();
            UserRank = new Rank();
            Username = new List<string>();
            UsernameClean = new List<string>();
            Followers = new List<User>();
            Stats = new GDStats();
            Medals = new List<Medal>();
            
            // Properties Setter //
            Dates.DateRegistered = DateTime.Now;
            Dates.LastVisit = DateTime.Now;

            var country = Country.Countries.Find(c => c.Acronym == RegionInfo.CurrentRegion.Name);
            
            //Check if the country was already made, if not, create that country
            if (country != null)
                Country = country;
            else
                Country.CreateCountry(RegionInfo.CurrentRegion);
        }

        // Functions //
        /// <summary>Changes the username and clears out all the rest.</summary>
        /// <param name="newUsername">The new Username that the <see cref="User"/> will be given.</param>
        /// <param name="newCleanUsername">The new Cleaner Username the <see cref="User"/> will be given.</param>
        private void ChangeUsername(string newUsername, [CanBeNull] string newCleanUsername)
        {
            if (Username.Count >= 5)
                Username.Remove(Username[4]);
            
            if (UsernameClean.Count >= 5)
                UsernameClean.Remove(Username[4]);
            
            Username.Add(newUsername);
            if (newCleanUsername != null) UsernameClean.Add(newCleanUsername);
        }
        
        /// <summary>Reverts back to the previous Username.</summary>
        /// <param name="index">What index should it go back to?</param>
        /// <param name="affectCleanUsername">should it affect the CleanUsername parameter as well?</param>
        private void RevertUsername(int index, bool affectCleanUsername)
        {
            if (index > 5)
                return;

            var newUsername = Username[index];
            var newCleanUsername = UsernameClean[index];

            ChangeUsername(newUsername, affectCleanUsername ? newCleanUsername : null);
        }
        
        /// <summary>Generates a new Token comprised of random letters and numbers.</summary>
        /// <returns>Random numbers and letters.</returns>
        private string GenerateToken()
        {
            const int length = 20;
            return Token = RandomString(length);
        }

        /// <summary>Refreshes the encryption method, used to prevent hacking of the account.</summary>
        private void RefeshEncryption()
        {
            var newToken = GenerateToken();
            
            //TODO: Encrypt the private data with the given Token to be used as the password.
        }

        /// <summary>Generates a random string within the given length,</summary>
        /// <param name="length">How long should the String be?</param>
        /// <returns>Random numbers and letters.</returns>
        private static string RandomString(int length)
        {
            const string chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890<>?/\;:[]{}-=_+`~";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[RNG.Next(s.Length)]).ToArray());
        }
        
        /// <summary>Gives the <see cref="User"/> a new <see cref="Medal"/>.</summary>
        /// <param name="medal">The <see cref="Medal"/> to be given.</param>
        public void GiveMedal(Medal medal) =>
            Medals.Add(medal);

        /// <summary>Removes a <see cref="Medal"/> from the <see cref="User"/>.</summary>
        /// <param name="medal"><see cref="Medal"/> to be removed.</param>
        public void RemoveMedal(Medal medal) =>
            Medals.Remove(medal);
    }
    
    public struct GDStats
    {
        /// <summary>GD Stars.</summary>
        public int Stars { get; set; }

        /// <summary>GD Diamonds.</summary>
        public int Diamond { get; set; }

        /// <summary>GD Official Coins.</summary>
        public int OfficialCoins { get; set; }

        /// <summary>GD User Coins.</summary>
        public int UserCoins { get; set; }

        /// <summary>GD Demons</summary>
        public int Demons { get; set; }

        /// <summary>GD Creator Points (Only affects Creators and Country Ranking)</summary>
        public int CreatorPoints { get; set; }
    }
}