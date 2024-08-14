using System.ComponentModel.DataAnnotations;

namespace TechStore.Auth.Payload
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
