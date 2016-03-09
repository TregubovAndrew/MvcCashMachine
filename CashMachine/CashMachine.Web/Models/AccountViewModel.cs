using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class AccountViewModel
    {
        public string CardNumber { get; set; }

        public int PinCode { get; set; }

        public decimal Money { get; set; }


    }
}