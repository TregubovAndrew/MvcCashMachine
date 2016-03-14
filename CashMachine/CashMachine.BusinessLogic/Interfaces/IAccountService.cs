using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.BusinessLogic.Services;
using CashMachine.DataAccess.Entities;

namespace CashMachine.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Account GetAccountById(int id);

        Account GetAccountByCardNumber (string cardNumber);

        void BlockAccount(Account account);

        AccountService.AuthResult Auth(string cardNumber, string pinCode);

        Operation WithdrawMoneyAndGetOperation(Account account, decimal sum);

        Operation BalanceOperation(Account account);

        void UnBlockAccount(Account account);
    }
}
