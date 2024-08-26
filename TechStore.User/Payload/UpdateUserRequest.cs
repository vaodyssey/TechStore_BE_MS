using System.ComponentModel.DataAnnotations;

namespace TechStore.User.Payload
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Please provide a userId.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide a valid email address.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Have you entered your password?")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Have you entered your phone number?")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Your address seems to be missing.")]
        public string Address { get; set; }
    }
}
