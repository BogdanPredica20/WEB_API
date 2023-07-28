using System.ComponentModel.DataAnnotations;

namespace BogdanPredica_API.Models.Authentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
