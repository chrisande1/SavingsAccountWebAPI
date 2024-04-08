using Mapster;
using Microsoft.AspNetCore.Mvc;
using SavingsAccountWebAPI.DTOs;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;
using SavingsAccountWebAPI.Services.Repository;


namespace SavingsAccountWebAPI.Controllers
{
    [ApiController]
    [Route("api/Transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRespository _accountRespository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRespository accountRespository)
        {
            _transactionRepository = transactionRepository;
            _accountRespository = accountRespository;
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction(TransactionRequestDTO request)
        {
            // Retrieve the account to update
            var account = await _accountRespository.GetAccountByAccountNumber(request.AccountNumber);

            // Validate request data (e.g., account existence)
            if (account == null)
            {
                return BadRequest("Account not found.");
            }

            var transaction = new Transaction
            {

                Amount = request.Amount,
                TransactionType = request.TransactionType,
                TransactionDate = DateTime.Now.ToString(),
                Account = account

            };

            // Validate transaction amount (assuming non-negative amount)
            if (request.Amount <= 0)
            {
                return BadRequest("Transaction amount must be positive.");
            }

            if (request.TransactionType == TransactionType.Withdrawal && account.CurrentBalance < request.Amount)
            {
                return BadRequest("Insufficient funds for withdrawal.");// Handle insufficient funds
            }

            // Update account balance 
            await _accountRespository.UpdateBalance(account, request.Amount, request.TransactionType);


            var result = await _transactionRepository.Create(transaction);
            var createdTransaction = result.Adapt<TransactionResponseDTO>();
            return Ok(createdTransaction);
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var result = await _transactionRepository.GetAllTransactions();
            
            return Ok(result.Adapt<List<TransactionResponseDTO>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var result = await _transactionRepository.GetTransactionById(id);
            return Ok(result.Adapt<TransactionResponseDTO>());
        }

        [HttpGet("AccountNumber")]
        public async Task<IActionResult> GetAllTransactionsByAccountNumber(string AccountNumber)
        {
            var result = await _transactionRepository.GetAllTransactionsByAccountNumber(AccountNumber); 
            
            return Ok(result.Adapt<List<TransactionResponseDTO>>());
        }


    }
}
