using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GDE.Web.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = "";

        public ProfileInfo Profile { get; set; } = new ProfileInfo();
        
        public class ProfileInfo
        {
            public string CurrentLocation { get; set; }

            public string Interests { get; set; }

            public string Occupation { get; set; }

            public string Twitter { get; set; }

            public string Discord { get; set; }

            public string Youtube { get; set; }

            public string Twitch { get; set; }

            public string Website { get; set; }
        }

        public byte[] Avatar { get; set; }
        
        public byte[] Cover { get; set; }

        public Playstyle[] Playstyles { get; set; } = new List<Playstyle>().ToArray();

        public enum Playstyle
        {
            Keyboard,
            Mouse,
            Controller,
            Touch
        }

        public SettingsInfo Settings { get; set; } = new SettingsInfo();
        
        public class SettingsInfo
        {
            public PrivacyInfo Privacy { get; set; } = new PrivacyInfo();
        
            public class PrivacyInfo
            {
                public bool FriendsOnly { get; set; }

                public bool HidePresence { get; set; }
            }

            public NotificationInfo Notifications { get; set; } = new NotificationInfo();
        
            public class NotificationInfo
            {
                public bool AutoEnable { get; set; }
            }
        }

        // make sure this is kept safe at all times
        public string Token { get; set; }
    }
}