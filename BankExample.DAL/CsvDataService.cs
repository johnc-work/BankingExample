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
    public class CsvDataService : IDataService
    {
        private string BankName { get; set; }
        CsvConfiguration csvConfig;
        private static string storePath;
        public CsvDataService(string bankName)
        {
            BankName = bankName;
            storePath = $"{BankName}_accounts.csv";//would want to pass a config value here to speicify the path normally
            if (!File.Exists(storePath)) File.WriteAllText(storePath, $"{BankName},123,1,,45,John C\r\n{BankName},234,2,1,314,Raymond C");//this is not good, but it'll do for now
            csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Comment = '#',
                AllowComments = true,
                Delimiter = ",",
            };
        }
        public void UpdateAccount(AccountModel account)
        {
            var accounts = LoadAccountsFromFile();
            for (var i = 0; i < accounts.Length - 1; i++)
            {
                var acct = accounts[i];
                if (acct.AccountNumber == account.AccountNumber)
                {
                    accounts[i] = account;
                }
            }

            SaveAll(accounts);
        }

        public void UpdateAccount(string AccountNumber, double Balance)
        {
            var accounts = LoadAccountsFromFile();
            foreach (var account in accounts)
            {
                if (account.AccountNumber == AccountNumber)
                {
                    account.Balance = Balance;
                }
            }

            SaveAll(accounts);

        }
        public AccountModel GetAccount(string AccountNumber)
        {
            var accounts = LoadAccountsFromFile();
            foreach (var account in accounts)
            {
                if (account.AccountNumber == AccountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        private AccountModel[] LoadAccountsFromFile()
        {
            using var streamReader = File.OpenText(storePath);
            using var csvReader = new CsvReader(streamReader, csvConfig);
            csvReader.Read();
            var accounts = new Stack<AccountModel>();
            while (csvReader.Read())
            {
                var account = new AccountModel();
                account.BankName = csvReader.GetField(0);
                account.AccountNumber = csvReader.GetField(1);
                account.AccountType = csvReader.GetField<int>(2);
                account.InvestmentAccountType = csvReader.GetField<int?>(3);
                account.Balance = csvReader.GetField<double>(4);
                account.OwnerName = csvReader.GetField(5);

                accounts.Push(account);
            }
            return accounts.ToArray();
        }
        private void SaveAll(IEnumerable<AccountModel> accounts)
        {
            using var writer = new StreamWriter(storePath);
            using var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

            csvWriter.WriteHeader<AccountModel>();
            csvWriter.NextRecord(); // /r/n between records and  header
            csvWriter.WriteRecords(accounts);

        }

    }
}
