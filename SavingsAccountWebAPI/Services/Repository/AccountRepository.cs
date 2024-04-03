using SavingsAccountWebAPI.Data;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;

namespace SavingsAccountWebAPI.Services.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRespository
    {
        public AccountRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
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
