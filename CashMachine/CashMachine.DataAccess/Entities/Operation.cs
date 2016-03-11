using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.DataAccess.Entities
{
    public class Operation
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string CodeOfOperation { get; set; }

        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }

    }
}
