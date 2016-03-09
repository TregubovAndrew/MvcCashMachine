using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;

namespace CashMachine.DataAccess.Repositories
{
    public class OperationHistoryRepository : IOperationHistoryRepository
    {
        private readonly CashMachineDbContext _db;

        public OperationHistoryRepository(CashMachineDbContext db)
        {
            _db = db;
        }

        public void CreateOperationHistory(OperationHistory operationHistory)
        {
            _db.OperationHistories.Add(operationHistory);
            _db.SaveChanges();
        }
    }
}
