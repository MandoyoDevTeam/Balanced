using System;
using System.Collections.Generic;
using Balanced.Config;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Balanced.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class CreditTest
    {

         public CreditTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
         public void Connect_Credit_Rest()
        {
            var creditService = new CreditService();
            var items = creditService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        public void Create_Credit()
        {
            var creditService = new CreditService();
            var bankAccountService = new BankAccountService();
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var creditSent = new Credit
            {                   
                Amount = 1000,
                Description = "Test Credit",
                AppearsOnStatementAs = "Test"
            };

            var creditReceived = creditService.Create(creditSent,bankAccountReceived.BankAccounts[0]);

            Assert.IsNotNull(creditReceived);
            Assert.IsNotNull(creditReceived.Credits);
            Assert.IsTrue(creditReceived.Credits.Count > 0);
            Assert.IsNotNull(creditReceived.Credits[0].Id);
            Assert.IsTrue(creditReceived.Credits[0].Amount == creditSent.Amount);
            Assert.IsTrue(string.Compare(creditReceived.Credits[0].Description, creditSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(creditReceived.Credits[0].AppearsOnStatementAs, creditSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Credit()
        {
            var creditService = new CreditService();
            var credit = creditService.Get(new Credit { Id = BalancedTestKeys.CreditTestId });

            Assert.IsNotNull(credit);
            Assert.IsNotNull(credit.Credits);
            Assert.IsTrue(credit.Credits.Count > 0);
            Assert.IsTrue(credit.Credits[0].Id == BalancedTestKeys.CreditTestId);
        }

        [TestMethod]
        public void List_Credits()
        {
            var creditService = new CreditService();
            var credit = creditService.List();

            Assert.IsNotNull(credit);
            Assert.IsNotNull(credit.Credits);
            Assert.IsTrue(credit.Credits.Count > 0);
        }

        [TestMethod]
        public void List_BankAccount_Credits()
        {
            var creditService = new CreditService();
            var bankAccountService = new BankAccountService();

            var bankAccount = bankAccountService.Get(new BankAccount {Id = BalancedTestKeys.BankAccountTestId });

            var credit = creditService.ListForBankAccount(bankAccount.BankAccounts[0]);

            Assert.IsNotNull(credit);
            Assert.IsNotNull(credit.Credits);
            Assert.IsTrue(credit.Credits.Count > 0);
        }

        [TestMethod]
        public void Update_Credit()
        {
            var creditService = new CreditService();
            var credit = creditService.Get(new Credit { Id = BalancedTestKeys.CreditTestId });

            credit.Credits[0].Description = "whatever";
            credit.Credits[0].Meta = new Dictionary<string, string> { { "key", "value" } };

            var creditUpdated = creditService.Update(credit.Credits[0]);

            Assert.IsNotNull(creditUpdated);
            Assert.IsNotNull(creditUpdated.Credits);
            Assert.IsTrue(creditUpdated.Credits.Count > 0);
            Assert.IsTrue(string.Compare(creditUpdated.Credits[0].Description, "whatever",StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(creditUpdated.Credits[0].Meta);
            Assert.IsTrue(creditUpdated.Credits[0].Meta.Count > 0);
            Assert.IsTrue(creditUpdated.Credits[0].Meta.ContainsKey("key"));
            Assert.IsTrue(string.Compare(creditUpdated.Credits[0].Meta["key"], "value", StringComparison.InvariantCultureIgnoreCase) == 0);
            
        }
    }
}
