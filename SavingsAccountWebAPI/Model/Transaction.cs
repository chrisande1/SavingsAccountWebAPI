namespace SavingsAccountWebAPI.Model
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
    public class Transaction : Entity
    {
        public TransactionType TransactionType { get; set; }
        public float? Amount { get; set; }
        public string? TransactionDate { get; set; }
        public Account Account { get; set; } 
    }
}
