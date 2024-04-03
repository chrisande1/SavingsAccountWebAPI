using SavingsAccountWebAPI.Model;

namespace SavingsAccountWebAPI.Services.Interface
{
    public interface IAccountRespository : IGenericRepository<Account>
    {
        public Task<Account?> Update(Guid ID, Account account);



    }
}
