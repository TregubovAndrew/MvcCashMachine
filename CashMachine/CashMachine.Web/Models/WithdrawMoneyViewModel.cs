using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Models
{
    public class WithdrawMoneyViewModel
    {
        [Range(1, Double.MaxValue, ErrorMessage = "Сума для снятия должна быть больше нуля")]
        public decimal Money { get; set; }

    }
}