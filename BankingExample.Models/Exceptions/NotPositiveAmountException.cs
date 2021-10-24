using System;
using System.Collections.Generic;
using System.Text;

namespace BankingExample.Models
{
    public class NotPositiveAmountException : Exception
    {
        public NotPositiveAmountException() : base("Transfers can only be in positive, non-zero incremenets.")
        {
        }
    }
}
