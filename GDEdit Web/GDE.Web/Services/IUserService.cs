using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using GDE.Web.Entities;
using GDE.Web.Extensions;
using GDE.Web.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static GDE.Web.Data.Database.AppDatabase;

namespace GDE.Web.Services
{
    public interface IUserService
    {
        User Login(string username, string password);
        User Register(string username, string password, string email);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> users
        {
            get
            {
                var table = Database.Database.Table("Accounts");

                return table.RunResultAsync<List<User>>(Database.Connection).Result;
            }
        }

        private readonly string secret = ConfigurationManager.AppSettings["jwt_key"];

        public User Login(string username, string password)
        {
            var user = users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            createToken(user, "*");

            return user.WithoutPassword();
        }

        public User Register(string username, string password, string email)
        {
            // Let's make sure some info is correct.
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                return null;
            
            var existingUser = users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (existingUser != null)
                return null;

            var user = new User
            {
                Username = username,
                Password = password,
                Email    = email,
                Id = users.Count + 1
            };
            
            var table = Database.Database.Table("Accounts");
            table.Insert(user).RunWrite(Database.Connection).Dump();
            
            createToken(user, "*");

            return user.WithoutPassword();
        }

        private void createToken(User user, string scope = "")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key          = Encoding.ASCII.GetBytes(secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("scopes", $"[{scope}]")
                }),
                Expires            = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
        }
        
        public IEnumerable<User> GetAll()
        {
            return users.WithoutPasswords();
        }
    }
}