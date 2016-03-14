using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class BalanceViewModel
    {
        public string CardNumber { get; set; }

        public decimal Money { get; set; }

        public DateTime DateTime { get; set; }

    }
}