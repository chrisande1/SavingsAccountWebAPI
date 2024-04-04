using Microsoft.EntityFrameworkCore;
using SavingsAccountWebAPI.Data;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;

namespace SavingsAccountWebAPI.Services.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByAccount(Account account)
        {
            return await _dBContext.Transactions
                .Include(transaction => transaction.Account)
                .Where(transaction => transaction.Account == account && transaction.DeletedAt != null)
                .ToListAsync();

        }


        public async Task<IEnumerable<Transaction>> GetTransactionById(Guid Id)
        {
            return await _dBContext.Transactions
                .Include(transaction => transaction.Account)
                .Where(transaction => transaction.Id == Id && transaction.DeletedAt != null)
                .ToListAsync();
        }

    } 
}
