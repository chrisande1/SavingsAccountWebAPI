namespace SavingsAccountWebAPI.DTOs
{
    public record UserRequestDTO
    (
        string Name,
        string Email,
        string Password
        );
}
