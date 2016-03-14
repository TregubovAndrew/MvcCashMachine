using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Exceptions
{
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException()
        {
        }

        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}