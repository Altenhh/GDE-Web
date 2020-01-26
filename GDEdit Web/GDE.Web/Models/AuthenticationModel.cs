using System.ComponentModel.DataAnnotations;

namespace GDE.Web.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}