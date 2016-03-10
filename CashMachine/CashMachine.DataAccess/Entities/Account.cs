using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.DataAccess.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public int PinCode { get; set; }

        public decimal AvailableBalance { get; set; }

        public bool IsBlocked { get; set; }

        public int AttemptsCount { get; set; }

        public DateTime? DateOfLastFailedAttempt { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }

    }
}
