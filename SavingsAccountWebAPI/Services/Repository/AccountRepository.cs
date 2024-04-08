using Microsoft.EntityFrameworkCore;
using SavingsAccountWebAPI.Data;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;
using System;

namespace SavingsAccountWebAPI.Services.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRespository
    {
        public AccountRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }

        public async Task UpdateBalance(Account account, float amount, TransactionType transactionType)
        {
            
            

            // 1. Updating the account's CurrentBalance property
            account.CurrentBalance += transactionType == TransactionType.Deposit ? amount : -amount;
            account.UpdatedAt = DateTime.Now.ToString();

            // 2. Marking the account for update (optional)
            _dBContext.Accounts.Update(account);

            // 3. Saving changes to the database
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
                .Where(account => account.DeletedAt == null)
                .FirstOrDefaultAsync(account => account.Id == Id);
        }
        public async Task<Account?> GetAccountByAccountNumber(string AccountNumber)
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

        public async Task<bool> IsAccountNumberExists(string accountNumber)
        {
            return await _dBContext.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<string> GenerateUniqueAccountNumber()
        {
            var random = new Random(); // Create an instance of Random
            string accountNumber;
            do
            {
                // Generate random digits
                char[] digits = new char[14];
                for (int i = 0; i < 14; i++)
                {
                    digits[i] = (char)random.Next(48, 58);
                }

                // Format with hyphens (every 4 digits)
                accountNumber = string.Empty;
                for (int i = 0; i < 14; i++)
                {
                    accountNumber += digits[i];
                    if (i == 3 || i == 7)
                    {
                        accountNumber += "-";
                    }
                }
            } while (await IsAccountNumberExists(accountNumber)); // Check for uniqueness

            return accountNumber;
        }




    }
}
