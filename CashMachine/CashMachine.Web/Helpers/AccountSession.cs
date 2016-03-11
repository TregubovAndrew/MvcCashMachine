using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Web.Helpers
{
    public class AccountSession
    {
        public string CardNumber { get; set; }

        public bool IsAuthenticated { get; set; }

        public AccountSession()
        {
            IsAuthenticated = false;
        }

    }
}