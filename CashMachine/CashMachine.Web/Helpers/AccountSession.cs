using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Helpers
{
    public class AccountSession
    {
         public string CardNumber { get; set; }

        public int PinCode { get; set; }

        public decimal Money { get; set; }

    }
}