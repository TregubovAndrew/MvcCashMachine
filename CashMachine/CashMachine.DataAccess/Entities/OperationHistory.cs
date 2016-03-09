using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.DataAccess.Entities
{
    public class OperationHistory
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime DateTime { get; set; }

        public string CodeOfOperation { get; set; }

        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }

    }
}
