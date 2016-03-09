using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class WithdrawMoneyReportViewModel
    {
        public string CardNumber { get; set; }

        public decimal WithdrawedSum { get; set; }

        public decimal Balance { get; set; }

        public DateTime Date { get; set; }
    }
}