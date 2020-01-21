using System.ComponentModel.DataAnnotations;

namespace GDE.Web.Models
{
    public class RegistrationModel : AuthenticationModel
    {
        [Required]
        public string Email { get; set; }
    }
}