﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SavingsAccountWebAPI.DTOs;
using SavingsAccountWebAPI.Model;
using SavingsAccountWebAPI.Services.Interface;


namespace SavingsAccountWebAPI.Controllers
{
    [ApiController]
    [Route("api/Accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRespository _accountRepository;

        public AccountController(IAccountRespository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Register"), Authorize]
        public async Task<IActionResult> CreateAccount(AccountCreateRequestDTO request)
        {
            var Account = new Account
            {
                AccountNumber = await _accountRepository.GenerateUniqueAccountNumber(),
                OwnerName = request.OwnerName,
                OpeningBalance = request.OpeningBalance,
                CurrentBalance = request.OpeningBalance
        };

            
            var CreatedAccount = await _accountRepository.Create(Account);
            return Ok(CreatedAccount);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllAccounts()
        {
            return Ok(await _accountRepository.GetAll());
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var targetAccount = await _accountRepository.GetAccountByAccountId(id);

            if (targetAccount == null)
            {
                return BadRequest("Account does not exist!");
            }

            return Ok(targetAccount);
        }

        [HttpGet("account/by-accountNumber/{accountNumber}")]
        [Authorize]
        public async Task<IActionResult> GetAccountByAccountNumber(string accountNumber)
        {
            var targetAccount = await _accountRepository.GetAccountByAccountNumber(accountNumber);

            if(targetAccount == null)
            {
                return BadRequest("Account does not exist!");
            }

            return Ok(targetAccount);
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> UpdateAccount(Guid id, AccountUpdateRequestDTO request)
        {
            var targetAccount = await _accountRepository.GetAccountByAccountId(id);


            if (targetAccount == null)
            {
                return BadRequest("Account does not exist!");
            }

            targetAccount = new Account
            {
                OwnerName = request.OwnerName,
                AccountNumber = targetAccount.AccountNumber,
                CurrentBalance = targetAccount.CurrentBalance
            };

            return Ok(await _accountRepository.Update(id, targetAccount));
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var targetAccount = await _accountRepository.GetAccountByAccountId(id);

            if (targetAccount == null)
            {
                return BadRequest("Account does not exist!");
            }

            return Ok(await _accountRepository.DeleteById(id));
        }


    }
}
