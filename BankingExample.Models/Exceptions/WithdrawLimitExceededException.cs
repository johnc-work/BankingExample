using System;

namespace BankingExample.Models
{
    public class WithdrawLimitExceededException : Exception
    {
        public WithdrawLimitExceededException() : base("Transfer withdraw limit exceeded.")
        {
        }
    }
}
