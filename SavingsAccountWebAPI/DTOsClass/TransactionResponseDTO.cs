using Azure.Core;
using Microsoft.Identity.Client;
using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.DTOsClass
{
    // Define the class
    public class TransactionResponseDTO
    {
        public Guid AccountId { get; set; }
        public Guid AccountNumber { get; set; }
        public TransactionType TransactionType { get; set; }
        public float Amount { get; set; } 
        public float CurrentBalance { get; set; }
        public string? TransactionDate { get; set; }
    }

}
