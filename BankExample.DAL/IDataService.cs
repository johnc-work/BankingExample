using BankExample.DAL.DataModels;
using System;

namespace BankExample.DAL
{
    public interface IDataService
    {
        void UpdateAccount(string AccountNumber, double Balance);
        AccountModel GetAccount(string AccountNumber);
    }
}
