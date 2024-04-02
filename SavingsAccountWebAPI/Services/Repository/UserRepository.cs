using Microsoft.EntityFrameworkCore;
using SavingsAccountWebAPI.Data;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;
using System.Text.RegularExpressions;

namespace SavingsAccountWebAPI.Services.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDBContext dBContext) : base(dBContext)
        {

        }

        public async Task<User> Update(Guid Id, User user)
        {
            var targetUser = await GetById(Id);

            targetUser.Name = user.Name;
            targetUser.Email = user.Email;
            targetUser.Password = user.Password;

            targetUser.UpdatedAt = DateTime.Now.ToString();
            await _dBContext.SaveChangesAsync();
            return targetUser;
        }
        public async Task<bool> CheckIfEmailExists(string Email)
        {
            return await _dBContext.Users.AnyAsync(u => u.Email == Email);
        }


        public async Task<User?> GetUserByEmail(string Email)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(user => user.Email == Email);
        }

        public async Task<bool> IsValidEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }

            
            const string emailRegexPattern = @"^\w+@\w+\.\w+$";
            return await Task.Run(() => Regex.IsMatch(Email, emailRegexPattern));
        }


    }
}
