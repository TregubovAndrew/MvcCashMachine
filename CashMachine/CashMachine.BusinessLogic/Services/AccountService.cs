using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.BusinessLogic.Interfaces;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;
using CashMachine.Web.Exceptions;
using CashMachine.Web.Helpers;

namespace CashMachine.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        private void EditAccount(Account account)
        {
            _accountRepository.EditAccount(account);
        }

        public Account GetAccountByCardNumber(string cardNumber)
        {
            return _accountRepository.GetAllAccounts().FirstOrDefault(a => a.CardNumber == cardNumber);
        }

        public void BlockAccount(Account account)
        {
            account.IsBlocked = true;
            EditAccount(account);
        }

        public void UnBlockAccount(Account account)
        {
            account.IsBlocked = false;
            account.AttemptsCount = 0;
            EditAccount(account);
        }

        public enum AuthResult
        {
            Success,
            Fail,
            Blocked
        }

        public AuthResult Auth(string cardNumber, string pinCode)
        {
            var account = GetAccountByCardNumber(cardNumber);
            const int maxAttemptsCount = 3;

            if (account.IsBlocked)
                return AuthResult.Blocked;
            if (account.PinCode == HashManager.HashPassword(pinCode))
            {
                account.AttemptsCount = 0;
                EditAccount(account);
                return AuthResult.Success;
            }
            if (account.AttemptsCount >= maxAttemptsCount && !account.IsBlocked)
            {
                account.AttemptsCount++;
                BlockAccount(account);
                return AuthResult.Blocked;
            }

            account.DateOfLastFailedAttempt = DateTime.Now;
            account.AttemptsCount++;
            EditAccount(account);
            return AuthResult.Fail;

        }

        public Operation WithdrawMoneyAndGetOperation(Account account, decimal sum)
        {
            if (sum <= 0)
                throw new ArgumentException("sum is less than zero");
            if (account.AvailableBalance - sum >= 0)
            {
                account.AvailableBalance -= sum;
                var operation = new Operation
                {
                    DateTime = DateTime.UtcNow,
                    CodeOfOperation = "снятие денег",
                    AccountId = account.Id
                };
                account.Operations.Add(operation);
                EditAccount(account);
                return operation;
            }

            throw new InsufficientFundsException("sum is more than available balance");

        }

        public Operation BalanceOperation(Account account)
        {
            var operation = new Operation
            {
                DateTime = DateTime.UtcNow,
                CodeOfOperation = "просмотр баланса",
                AccountId = account.Id
            };
            account.Operations.Add(operation);
            EditAccount(account);
            return operation;
        }
    }
}
