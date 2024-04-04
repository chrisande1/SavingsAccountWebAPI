namespace SavingsAccountWebAPI.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Insufficient funds for withdrawal.")
        {
        }
    }
}