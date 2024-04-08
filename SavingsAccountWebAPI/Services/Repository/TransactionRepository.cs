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

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _dBContext.Transactions
                .Include(transaction => transaction.Account)
                .Where(transaction => transaction.DeletedAt == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByAccountNumber(string AccountNumber)
        {
            return await _dBContext.Transactions
                .Include(transaction => transaction.Account)
                .Where(transaction => transaction.Account.AccountNumber == AccountNumber && transaction.DeletedAt == null)
                .ToListAsync();

        }


        public async Task<Transaction?> GetTransactionById(Guid Id)
        {
            return await _dBContext.Transactions
                .Include(transaction => transaction.Account)
                .Where(transaction => transaction.DeletedAt == null)
                .FirstOrDefaultAsync(transaction => transaction.Id == Id);
        }

    } 
}
