using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<Transaction?> GetTransactionById(Guid Id);
        public Task<IEnumerable<Transaction>> GetAllTransactionsByAccountNumber(Guid AccountNumber);
        public Task<IEnumerable<Transaction>> GetAllTransactions();
    }
}
