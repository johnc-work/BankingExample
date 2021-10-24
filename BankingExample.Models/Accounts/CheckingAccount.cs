
using BankExample.DAL.DataModels;

namespace BankingExample.Models
{
    internal class CheckingAccount : Account
    {
        public CheckingAccount() { }
        public CheckingAccount(AccountModel accountData)
        {

            var owner = new AccountHolder { Name = accountData.OwnerName };
            AccountNumber = accountData.AccountNumber;
            Balance = accountData.Balance;
            Owner = owner;
            DataService = DataService;
        }

    }
}
