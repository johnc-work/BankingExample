using System;
using System.Collections.Generic;
using System.Text;

namespace BankExample.DAL.DataModels
{
    public class AccountModel
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public int AccountType { get; set; }
        public int? InvestmentAccountType { get; set; }
        public double Balance { get; set; }
        public string OwnerName { get; set; }
    }
}
