using BankExample.DAL;
using BankExample.Service;
using BankingExample.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankExample.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAccountInfo()
        {
            var correctSummary = "Test Investment account #123\r\nBalance: 45";
            var bankName = "Test";
            var dal = new TestDataService(bankName);

            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            var summary = bank.GetAccountSummary("123");

            Assert.AreEqual(summary, correctSummary);
        }
        [TestMethod]
        public void GetMissingAccountInfo()
        {
            var correctSummary = "Account not found.";
            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);
            try
            {
                var summary = bank.GetAccountSummary("2183129837123");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(new AccountNotFoundException().GetType(), ex.GetType());
            }
        }
        [TestMethod]
        public void CheckDepost()
        {
            var originalValue = 45d;
            var correctTotal = 60d;
            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("123"), originalValue);
            bank.AccountDeposit("123", 15.0d);
            Assert.AreEqual(bank.GetBalance("123"), correctTotal);
        }
        [TestMethod]
        public void CheckWithdraw()
        {
            var originalValue = 45d;
            var correctTotal = 15d;
            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("123"), originalValue);
            bank.AccountWithdraw("123", 30.0d);
            Assert.AreEqual(bank.GetBalance("123"), correctTotal);
        }
        [TestMethod]
        public void CheckOverdraw()
        {
            var originalValue = 45d;
            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("123"), originalValue);
            try
            {
                bank.AccountWithdraw("123", 46.0d);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(new InsufficientFundsException(), ex);
            }
        }
        [TestMethod]
        public void CheckTransfer()
        {
            var account1start = 45d;
            var account1end = 15d;
            var account2start = 3000d;
            var account2end = 3030d;

            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("123"), account1start);
            Assert.AreEqual(bank.GetBalance("124"), account2start);
            bank.AccountTransfer("123", "124", 30);

            Assert.AreEqual(bank.GetBalance("123"), account1end);
            Assert.AreEqual(bank.GetBalance("124"), account2end);
        }
        [TestMethod]
        public void TransferLimitTest()
        {
            var account2start = 3000d;

            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("124"), account2start);
            try { 
                bank.AccountWithdraw("124", 501);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(new WithdrawLimitExceededException().GetType(), ex.GetType());
            }

            Assert.AreEqual(bank.GetBalance("124"), account2start);
        }
        [TestMethod]
        public void CheckOverTransfer()
        {
            var account1start = 45d;
            var account2start = 3000d;

            var bankName = "Test";
            var dal = new TestDataService(bankName);
            var bankConfig = new BankConfig { WithdrawLimitForIndividualAccounnts = 500d };
            var bank = new BankService(bankName, dal, bankConfig);

            Assert.AreEqual(bank.GetBalance("123"), account1start);
            Assert.AreEqual(bank.GetBalance("124"), account2start);

            try
            {
                bank.AccountTransfer("123", "124", 50);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(new InsufficientFundsException().GetType(), ex.GetType());
            }
            Assert.AreEqual(bank.GetBalance("123"), account1start);
            Assert.AreEqual(bank.GetBalance("124"), account2start);
        }


    }
}
