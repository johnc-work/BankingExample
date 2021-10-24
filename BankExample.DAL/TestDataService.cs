using BankExample.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;

namespace BankExample.DAL
{

    /// <summary>
    /// A NOT ideal, unsecure and non-distributed but lightweight and portable datastore :D
    /// but theoretically you could throw this out on a fileshare and have multiple users accessing it
    /// </summary>
    public class TestDataService : IDataService
    {
        private string BankName { get; set; }
        public TestDataService(string bankName)
        {
            BankName = bankName;
        }

        Dictionary<string, AccountModel> accounts = new Dictionary<string, AccountModel>
        {
            { "123", new AccountModel { BankName = "Test", AccountNumber = "123", AccountType = 1, InvestmentAccountType = null, Balance = 45, OwnerName = "John C" } },
            { "124", new AccountModel { BankName = "Test", AccountNumber = "124", AccountType = 2, InvestmentAccountType = 1, Balance = 3000.00, OwnerName = "Tim S" } },
            { "125", new AccountModel { BankName = "Test", AccountNumber = "125", AccountType = 2, InvestmentAccountType = 2, Balance = 1251234.00, OwnerName = "Samuel H" } }
        };



        public void UpdateAccount(string AccountNumber, double Balance)
        {
            accounts[AccountNumber].Balance = Balance;
        }
        public AccountModel GetAccount(string AccountNumber) {
            if (accounts.ContainsKey(AccountNumber))
            {
                return accounts[AccountNumber];
            }
            else 
            { 
                return null; 
            }
        }

    }
}
