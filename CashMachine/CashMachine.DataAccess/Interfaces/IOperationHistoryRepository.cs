using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;

namespace CashMachine.DataAccess.Interfaces
{
    public interface IOperationHistoryRepository
    {
        void CreateOperationHistory(OperationHistory operationHistory);
    }
}
