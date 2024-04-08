using SavingsAccountWebAPI.Model;
using System.Transactions;

namespace SavingsAccountWebAPI.DTOs
{
    public record TransactionResponseDTO
    (
        Guid TransactionId,
        TransactionType TransactionType,
        string TransactionDate,
        float Amount,
        string Account

        );
}
