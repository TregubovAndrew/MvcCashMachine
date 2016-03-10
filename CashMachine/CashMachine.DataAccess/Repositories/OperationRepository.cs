using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;

namespace CashMachine.DataAccess.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly CashMachineDbContext _db;

        public OperationRepository(CashMachineDbContext db)
        {
            _db = db;
        }

        public void CreateOperation(Operation operation)
        {
            _db.Operations.Add(operation);
            _db.SaveChanges();
        }
    }
}
