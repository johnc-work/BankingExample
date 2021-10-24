using System;

namespace BankingExample.Models
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Account not found.")
        {
        }
    }
}
