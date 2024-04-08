using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.DTOs
{
    public record TransactionRequestDTO
    (
        string AccountNumber,
        TransactionType TransactionType,
        float Amount
     
        );
}
