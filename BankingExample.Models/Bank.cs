using BankExample.DAL;
using System;
using System.Collections.Generic;
using static BankingExample.Models.Reference;

namespace BankingExample.Models
{
    public class Bank
    {
        public Bank() { } 
        public Bank(BankConfig Config)
        {
            config = Config;
        }
        private BankConfig config { get; set; }
        public string Name { get; set; }
        public IDataService DataService { get; set; }

        public Account GetAccount(string accountId)
        {
            Account account;
            var accountData = DataService.GetAccount(accountId);
            if (accountData == null) throw new AccountNotFoundException();
            if (accountData.AccountType == (int)AccountTypes.Checking)
                account = new CheckingAccount(accountData);
            else if (accountData.AccountType == (int)AccountTypes.Investment 
                && accountData.InvestmentAccountType.HasValue)
                account = new InvestmentAccount(accountData, config.WithdrawLimitForIndividualAccounnts);
            else
                throw new Exception("Account record invalid. Please notify the bank to resolve this.");

            return account;
        }
        public void UpdateAccount(Account account)
        {
            DataService.UpdateAccount(account.AccountNumber, account.Balance);
        }

    }
}
