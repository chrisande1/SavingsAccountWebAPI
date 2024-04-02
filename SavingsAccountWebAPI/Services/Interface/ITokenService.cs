using SavingsAccountWebAPI.DTOs;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface ITokenService
    {
        public string CreateUserToken(UserResponseDTO user);
    }
}
