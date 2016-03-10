﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.BusinessLogic.Interfaces;
using CashMachine.DataAccess.Entities;
using CashMachine.DataAccess.Interfaces;

namespace CashMachine.BusinessLogic.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationHistoryRepository;

        public OperationService(IOperationRepository operationHistoryRepository)
        {
            _operationHistoryRepository = operationHistoryRepository;
        }

        public void CreateOperation(Operation operation)
        {
            _operationHistoryRepository.CreateOperation(operation);
        }
    }
}