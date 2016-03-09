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
    public class OperationHistoryService : IOperationHistoryService
    {
        private readonly IOperationHistoryRepository _operationHistoryRepository;

        public OperationHistoryService(IOperationHistoryRepository operationHistoryRepository)
        {
            _operationHistoryRepository = operationHistoryRepository;
        }

        public void CreateOperationHistory(OperationHistory operationHistory)
        {
            _operationHistoryRepository.CreateOperationHistory(operationHistory);
        }
    }
}
