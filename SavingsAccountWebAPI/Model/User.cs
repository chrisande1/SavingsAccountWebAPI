﻿namespace SavingsAccountWebAPI.Model
{
    public class User
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}