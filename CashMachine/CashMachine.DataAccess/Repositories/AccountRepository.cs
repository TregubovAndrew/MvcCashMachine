using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;

namespace CashMachine.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CashMachineDbContext _db;

        public AccountRepository(CashMachineDbContext db)
        {
            _db = db;
        }

        public Account GetAccountById(int id)
        {
            return _db.Accounts.Find(id);
        }

        public void EditAccount(Account account)
        {
            _db.Entry(account).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _db.Accounts;
        }
    }
}
