﻿using System.ComponentModel.DataAnnotations;

namespace TechStore.Auth.Payload
{
    public class RegisterRequest        
    {
        [Required]        
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone{ get; set; }
        [Required]
        public string Address{ get; set; }

    }
}
