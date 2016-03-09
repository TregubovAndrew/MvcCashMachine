using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DataAccess.Entities;

namespace CashMachine.BusinessLogic.Interfaces
{
    public interface IOperationHistoryService
    {
        void CreateOperationHistory(OperationHistory operationHistory);
    }
}
