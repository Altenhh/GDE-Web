using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using GDE.Web.Entities;
using GDE.Web.Extensions;
using GDE.Web.Helpers;
using Microsoft.IdentityModel.Tokens;
using static GDE.Web.Services.Database.AppDatabase;

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
                var table = DatabaseService.Database.Table("Accounts");

                return table.RunResultAsync<List<User>>(DatabaseService.Connection).Result;
            }
        }

        private readonly string secret = ConfigurationManager.AppSettings["jwt_key"];

        public User Login(string username, string password)
        {
            var user = users.FirstOrDefault(x => x.Username == username && compareHashedPasswords(password, x.Password));

            if (user == null)
                return null;

            createToken(user, "*");

            return user.WithoutPassword();
        }

        public User Register(string username, string password, string email)
        {
            var savedPassowrdHash = createHashedPassword(password);
            
            // Let's make sure some info is correct.
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(savedPassowrdHash))
                return null;
            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (!match.Success)
                return null;
            
            var existingUser = users.FirstOrDefault(x => x.Username == username && compareHashedPasswords(password, x.Password));

            if (existingUser != null)
                return null;

            var user = new User
            {
                Username = username,
                Password = savedPassowrdHash,
                Email    = email,
                Id = users.Count + 1,
                Dates = new User.DatesInfo
                {
                    DateRegistered = DateTime.Now,
                    LastVisit = DateTime.Now
                }
            };
            
            var table = DatabaseService.Database.Table("Accounts");
            table.Insert(user).RunWrite(DatabaseService.Connection).Dump();
            
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

        // Due to security reasons, these two functions have been replaced with basic comparison.
        private string createHashedPassword(string password)
        {
            return password;
        }

        private bool compareHashedPasswords(string passwordToCompare, string savedPasswordHash)
        {
            return passwordToCompare == savedPasswordHash;
        }
        
        public IEnumerable<User> GetAll()
        {
            return users.WithoutPrivateInformations();
        }
    }
}