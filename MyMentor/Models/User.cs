using System;

namespace MyMentor.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string UserType { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public User() { }
    }
}