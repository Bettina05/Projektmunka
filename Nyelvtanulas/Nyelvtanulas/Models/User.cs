﻿namespace Nyelvtanulas.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
