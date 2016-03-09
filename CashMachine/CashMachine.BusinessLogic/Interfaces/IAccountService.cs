using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;

namespace CashMachine.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Account GetAccountById(int id);

        void EditAccount(Account account);

        Account GetAccountByCardNumber (string cardNumber);

        void BlockAccount(Account account);
    }
}
