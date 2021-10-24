using BankExample.DAL;
using BankingExample.Models;
using System;

namespace BankExample.Service
{
    public class BankService
    {
        private Bank bank { get; set; }
        private IDataService data {get;set;}
        public BankService(string BankName, IDataService Data, BankConfig config)
        {
            data = Data;
            bank = new Bank(config) { Name = BankName, DataService = Data };
        }

        public double GetBalance(string accountNumber)
        {
            var account = bank.GetAccount(accountNumber);
            if (account == null) throw new AccountNotFoundException();
            return account.Balance;
        }
        public string GetAccountSummary(string accountNumber)
        {
            var account = bank.GetAccount(accountNumber);
            var accountType = account.AccountType == Reference.AccountTypes.Checking ? "Checking" : "Investment";
            var output = $"{bank.Name} {accountType} account #{account.AccountNumber}\r\nBalance: {account.Balance}";
            return output;
        }
        //todo create and delete accounts

        public void AccountDeposit(string AccountNumber, double Amount)
        {
            var account = bank.GetAccount(AccountNumber);
            account.DepositFunds(Amount);
            bank.UpdateAccount( account);
        }
        public void AccountWithdraw(string AccountNumber, double Amount)
        {
            var account = bank.GetAccount(AccountNumber);
            account.WithdrawFunds(Amount);
            bank.UpdateAccount(account);
        }
        public void AccountTransfer(string FromAccountNumber, string ToAccountNumber, double Amount)
        {
            var fromAccount = bank.GetAccount(FromAccountNumber);
            var toAccount = bank.GetAccount(ToAccountNumber);
            toAccount.TransferFunds(fromAccount, Amount);
            bank.UpdateAccount(fromAccount);
            bank.UpdateAccount(toAccount);

        }
    }
}
