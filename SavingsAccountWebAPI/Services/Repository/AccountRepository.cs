using Microsoft.EntityFrameworkCore;
using SavingsAccountWebAPI.Data;
using SavingsAccountWebAPI.Exceptions;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;

namespace SavingsAccountWebAPI.Services.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRespository
    {
        public AccountRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }

        public async Task UpdateBalance(Account account, float amount, TransactionType transactionType)
        {
            // 1. Using an Account instance for context
            if (transactionType == TransactionType.Withdrawal && account.CurrentBalance < amount)
            {
                throw new InsufficientFundsException(); // Handle insufficient funds
            }

            // 2. Updating the account's CurrentBalance property
            account.CurrentBalance += transactionType == TransactionType.Deposit ? amount : -amount;
            account.UpdatedAt = DateTime.Now.ToString();

            // 3. Marking the account for update (optional)
            _dBContext.Accounts.Update(account);

            // 4. Saving changes to the database
            await _dBContext.SaveChangesAsync();
        }





        public async Task<Account> Add(Account account)
        {
            _dBContext.Accounts.Add(account);
            await _dBContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountByAccountId(Guid Id)
        {
            return await _dBContext.Accounts
                .Where(account => account.DeletedAt != null)
                .FirstOrDefaultAsync(account => account.Id == Id);
        }
        public async Task<Account?> GetAccountByAccountNumber(Guid AccountNumber)
        {
            return await _dBContext.Accounts
                .Where(account => account.DeletedAt == null)
                .FirstOrDefaultAsync(account => account.AccountNumber == AccountNumber);
                
        }

        public async Task<Account?> Update(Guid Id, Account account)
        {
            var targetAccount = await GetById(Id);

            targetAccount.AccountNumber = account.AccountNumber;
            targetAccount.OwnerName = account.OwnerName;
            targetAccount.CurrentBalance = account.CurrentBalance;

            targetAccount.UpdatedAt = DateTime.Now.ToString();
            await _dBContext.SaveChangesAsync();
            return targetAccount;
        }
    }
}
