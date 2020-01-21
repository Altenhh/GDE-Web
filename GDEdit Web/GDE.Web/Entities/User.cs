﻿namespace GDE.Web.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        // make sure this is kept safe at all times
        public string Token { get; set; }
    }
}