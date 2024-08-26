using System.ComponentModel.DataAnnotations;

namespace TechStore.User.Payload
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
