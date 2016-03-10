using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.BusinessLogic.Interfaces;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;

namespace CashMachine.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOperationRepository _operationRepository;

        public AccountService(IAccountRepository accountRepository, IOperationRepository operationRepository)
        {
            _accountRepository = accountRepository;
            _operationRepository = operationRepository;
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
            account.AttemptsCount = 4;
            EditAccount(account);
        }

        public enum AuthResult
        {
            Success,
            Fail,
            Blocked
        }

        public AuthResult Auth(string cardNumber, int pinCode)
        {
            var account = GetAccountByCardNumber(cardNumber);
            //if (account.AttemptsCount > 4 && (DateTime.Now - account.DateOfLastFailedAttempt).Value.TotalHours <= 1 && !account.IsBlocked)
            //{
            //    BlockAccount(account);
            //    return AuthResult.Blocked;
            //}
            //if ((DateTime.Now - account.DateOfLastFailedAttempt).Value.TotalHours > 1 && account.IsBlocked && account.PinCode == pinCode)
            //{
            //    UnBlockAccount(account);
            //    account.AttemptsCount--;
            //    EditAccount(account);
            //    return AuthResult.Success;
            //}
            if (account.PinCode == pinCode)
            {
                account.AttemptsCount = 4;
                EditAccount(account);
                return AuthResult.Success;
            }
            if (account.AttemptsCount <= 1 && !account.IsBlocked)
            {
                account.AttemptsCount--;
                BlockAccount(account);
                return AuthResult.Blocked;
            }
            if (account.IsBlocked)
                return AuthResult.Blocked;

            account.DateOfLastFailedAttempt = DateTime.Now;
            account.AttemptsCount--;
            EditAccount(account);
            return AuthResult.Fail;

        }

        public bool WithdrawMoneySuccess(Account account, decimal sum)
        {
            if (account != null)
            {
                if (account.AvailableBalance - sum >= 0)
                {
                    account.AvailableBalance -= sum;
                    EditAccount(account);
                    _operationRepository.CreateOperation(new Operation
                    {
                        CardNumber = account.CardNumber,
                        DateTime = DateTime.Now,
                        CodeOfOperation = "снятие денег",
                        AccountId = account.Id
                    });
                    return true;
                }
            }
            return false;
        }

        public bool BalanceOperationSuccess(Account account)
        {
            if (account != null)
            {
                _operationRepository.CreateOperation(new Operation
                {
                    CardNumber = account.CardNumber,
                    DateTime = DateTime.Now,
                    CodeOfOperation = "просмотр баланса",
                    AccountId = account.Id
                });
                return true;
            }
            return false;
        }
    }
}
