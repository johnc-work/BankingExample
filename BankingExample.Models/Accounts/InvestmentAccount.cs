using BankExample.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using static BankingExample.Models.Reference;

namespace BankingExample.Models
{
    internal class InvestmentAccount : Account
    {

        public InvestmentAccount()
        {
        }
        public InvestmentAccount(AccountModel accountData)
        {

            var owner = new AccountHolder { Name = accountData.OwnerName };
            AccountNumber = accountData.AccountNumber;
            Balance = accountData.Balance;
            Owner = owner;
            DataService = DataService;
            InvestmentAccountType = (InvestmentAccountTypes)accountData.InvestmentAccountType.Value;
        }
        public InvestmentAccount(AccountModel accountData, double? withdrawlLimit = null)
        {
            var owner = new AccountHolder { Name = accountData.OwnerName };
            AccountNumber = accountData.AccountNumber;
            Balance = accountData.Balance;
            Owner = owner;
            DataService = DataService;
            InvestmentAccountType = (InvestmentAccountTypes)accountData.InvestmentAccountType.Value;
            WithdrawlLimit = InvestmentAccountType == InvestmentAccountTypes.Individual ? withdrawlLimit : null ;
        }

        public InvestmentAccountTypes InvestmentAccountType { get; set; }
        public double? WithdrawlLimit { get; set; }
        public override void WithdrawFunds(double Amount)
        {
            if (Amount <= 0) throw new NotPositiveAmountException();
            if (WithdrawlLimit.HasValue && Amount > WithdrawlLimit) throw new WithdrawLimitExceededException();

            Balance -= Amount;
        }
    }
}
