using System;

namespace BankingExample.Models
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base("Transfer failed due to insufficient funds.")
        {
        }
    }
}
