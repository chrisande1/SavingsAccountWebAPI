using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.DTOs
{
    public record TransactionRequestDTO
    (
        Guid AccountNumber,
        TransactionType TransactionType,
        float Amount
     
        );
}
