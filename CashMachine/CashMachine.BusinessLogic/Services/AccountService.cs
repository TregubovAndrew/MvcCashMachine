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

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public void EditAccount(Account account)
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
    }
}
