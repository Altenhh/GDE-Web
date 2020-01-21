using System.Collections.Generic;
using System.Linq;
using GDE.Web.Entities;

namespace GDE.Web.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPrivateInformations(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPrivateInformation());
        }
        
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPrivateInformation(this User user)
        {
            var withoutPassword = user.WithoutPassword();
            withoutPassword.Settings = null;

            return withoutPassword;
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;

            return user;
        }
    }
}