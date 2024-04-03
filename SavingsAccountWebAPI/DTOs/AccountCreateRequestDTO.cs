namespace SavingsAccountWebAPI.DTOs
{
    public record AccountCreateRequestDTO
    (
        string OwnerName,
        float OpeningBalance
        );
}