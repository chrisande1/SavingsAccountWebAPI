﻿using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface IUserRepository :IGenericRepository<User>
    {
        public Task<User> Update(Guid Id, User user);
        public Task<User?> GetUserByEmail(string Email);
        public Task<bool> CheckIfEmailExists(string Email);
        public Task<bool> IsValidEmail(string Email);
    }
}