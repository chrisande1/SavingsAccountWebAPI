using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface IAccountRespository : IGenericRepository<Account>
    {
        public Task<Account> Add(Account account);
        public Task<Account?> Update(Guid Id, Account account);
        public Task<Account?> GetAccountByAccountId(Guid Id);
        public Task<Account?> GetAccountByAccountNumber(string AccountNumber);
        public Task<string> GenerateUniqueAccountNumber();
        public Task UpdateBalance(Account account, float amount, TransactionType transactionType);

    }
}
