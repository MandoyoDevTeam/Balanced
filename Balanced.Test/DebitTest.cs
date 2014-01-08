using System;
using System.Collections.Generic;
using Balanced.Config;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class DebitTest
    {
        public DebitTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
         public void Connect_Debit_Rest()
        {
            var debitService = new DebitService();
            var items = debitService.List();

            Assert.IsNotNull(items);

        }

       
        [TestMethod]
        public void Create_Debit_BankAccount()
        {
            var debitService = new DebitService();
            var bankAccountService = new BankAccountService();
            
            var bankAccount = bankAccountService.Get(new BankAccount { Id = BalancedTestKeys.BankAccountTestId });

            var debitSent = new Debit
            {
                Amount = 1000,
                Description = "Test Debit",
                AppearsOnStatementAs = "Test"
            };

            var debitReceived = debitService.Create(debitSent, bankAccount.BankAccounts[0]);

            Assert.IsNotNull(debitReceived);
            Assert.IsNotNull(debitReceived.Debits);
            Assert.IsTrue(debitReceived.Debits.Count > 0);
            Assert.IsNotNull(debitReceived.Debits[0].Id);
            Assert.IsTrue(debitReceived.Debits[0].Amount == debitSent.Amount);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].Description, debitSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].AppearsOnStatementAs, debitSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Create_Debit_Card()
        {
            var debitService = new DebitService();
            var cardService = new CardService();
            
            var card = cardService.Get(new Card { Id = BalancedTestKeys.CardTestId });

            var debitSent = new Debit
            {
                Amount = 1000,
                Description = "Test Debit",
                AppearsOnStatementAs = "Test"
            };

            var debitReceived = debitService.Create(debitSent, card.Cards[0]);

            Assert.IsNotNull(debitReceived);
            Assert.IsNotNull(debitReceived.Debits);
            Assert.IsTrue(debitReceived.Debits.Count > 0);
            Assert.IsNotNull(debitReceived.Debits[0].Id);
            Assert.IsTrue(debitReceived.Debits[0].Amount == debitSent.Amount);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].Description, debitSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].AppearsOnStatementAs, debitSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Debit()
        {
            var debitService = new DebitService();
            var debit = debitService.Get(new Debit { Id = BalancedTestKeys.DebitTestId });

            Assert.IsNotNull(debit);
            Assert.IsNotNull(debit.Debits);
            Assert.IsTrue(debit.Debits.Count > 0);
            Assert.IsTrue(debit.Debits[0].Id == BalancedTestKeys.DebitTestId);
        }

        [TestMethod]
        public void List_Debits()
        {
            var debitService = new DebitService();
            var items = debitService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Debits);
            Assert.IsTrue(items.Debits.Count > 0);
        }

        [TestMethod]
        public void Update_Debit()
        {
            var debitService = new DebitService();

            var debitReceived = debitService.Get(new Debit {Id = BalancedTestKeys.DebitTestId});

            debitReceived.Debits[0].Description = "Updated Description";
            debitReceived.Debits[0].Meta = new Dictionary<string, string>
            {
                {"testkey1", "testvalue1"},
                {"testkey2", "testvalue2"}
            };

            var debitUpdated = debitService.Update(debitReceived.Debits[0]);

            Assert.IsNotNull(debitUpdated);
            Assert.IsNotNull(debitUpdated.Debits);
            Assert.IsTrue(debitUpdated.Debits.Count > 0);
            Assert.IsTrue(debitReceived.Debits[0].Amount == debitUpdated.Debits[0].Amount);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].Id, debitUpdated.Debits[0].Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitReceived.Debits[0].Description, debitUpdated.Debits[0].Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(debitUpdated.Debits[0].Meta);
            Assert.IsTrue(debitUpdated.Debits[0].Meta.Count > 0);
            Assert.IsNotNull(debitUpdated.Debits[0].Meta["testkey1"]);
            Assert.IsNotNull(debitUpdated.Debits[0].Meta["testkey2"]);
            Assert.IsTrue(string.Compare(debitUpdated.Debits[0].Meta["testkey1"], debitReceived.Debits[0].Meta["testkey1"], StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitUpdated.Debits[0].Meta["testkey2"], debitReceived.Debits[0].Meta["testkey2"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
