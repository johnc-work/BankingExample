using BankExample.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using static BankingExample.Models.Reference;

namespace BankingExample.Models
{
    public abstract class Account
    {
        public IDataService DataService {get;set;}

        public string AccountNumber { get; set; }
        public double Balance { get; set; }

        public AccountTypes AccountType { get;  }

        public AccountHolder Owner { get; set; }

        public virtual void DepositFunds(double Amount)
        {
            if (Amount <= 0) throw new NotPositiveAmountException();
            Balance += Amount;
        }

        public virtual void WithdrawFunds(double Amount)
        {
            if (Amount <= 0) throw new NotPositiveAmountException();
            Balance -= Amount;
        }

        public virtual void TransferFunds(Account FromAccount, double Amount)
        {
            if (Amount <= 0) throw new NotPositiveAmountException();
            if (FromAccount.Balance <= Amount) throw new InsufficientFundsException();
            FromAccount.Balance -= Amount; //business rules as given don't specify if transfer involves the withdraw limit if so we'd override, going with the most literal interpretation, sorry
            Balance += Amount;

        }
    }
}
