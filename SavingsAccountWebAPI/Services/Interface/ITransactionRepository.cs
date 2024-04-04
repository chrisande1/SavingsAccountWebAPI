using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<IEnumerable<Transaction>> GetTransactionById(Guid Id);
        public Task<IEnumerable<Transaction>> GetAllTransactionsByAccount(Account account);
    }
}
