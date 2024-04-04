using Microsoft.AspNetCore.Mvc;
using SavingsAccountWebAPI.DTOs;
using SavingsAccountWebAPI.DTOsClass;
using SavingsAccountWebAPI.Exceptions;
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

            // Validate transaction amount (assuming non-negative amount)
            if (request.Amount <= 0)
            {
                return BadRequest("Transaction amount must be positive.");
            }

            // Update account balance (assuming UpdateBalance handles logic)
            await _accountRespository.UpdateBalance(account, request.Amount, request.TransactionType);

            // Create a transaction response object
            var transactionResponse = new TransactionResponseDTO
            {
                AccountId = account.Id, // Assuming you want AccountId in the response
                AccountNumber = account.AccountNumber,
                TransactionType = request.TransactionType,
                Amount = request.Amount,
                CurrentBalance = account.CurrentBalance,
                TransactionDate = DateTime.Now.ToString()
            };

            // Return the transaction response
            return Ok(transactionResponse);
        }


    }
}
