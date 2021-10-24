using System;
using System.Collections.Generic;
using System.Text;

namespace BankingExample.Models
{
    public class BankConfig
    {
        public double? WithdrawLimitForIndividualAccounnts {get;set;}
        //add more or less depending on business requirements
        //would likely live in a db table, beyond the scope of this proj
    }
}
