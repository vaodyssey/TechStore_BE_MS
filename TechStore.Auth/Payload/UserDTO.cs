﻿namespace TechStore.Auth.Payload
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }        
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
