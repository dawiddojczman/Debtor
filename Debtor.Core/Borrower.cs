using System;
using System.Collections.Generic;
using System.Text;

namespace Debtor.Core
{
    public class Borrower
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Interest { get; set; }

        public override string ToString()
        {
            return Name + ";" + Amount.ToString();
        }
    }
}
