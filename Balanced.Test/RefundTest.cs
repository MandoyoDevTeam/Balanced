using System;
using System.Collections.Generic;
using Balanced.Config;
using Balanced.Entities;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class RefundTest
    {

        public RefundTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
         public void Connect_Refund_Rest()
        {
            var refundService = new RefundService();
            var items = refundService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        public void Create_Refund()
        {
            var refundService = new RefundService();
            var debitService = new DebitService();
            var bankAccountService = new BankAccountService();
            
            var bankAccount = bankAccountService.Get(new BankAccount { Id = BalancedTestKeys.BankAccountTestId });


            var debitSent = new Debit
            {
                
                Amount = 1000,
                Description = "Test Debit",
                AppearsOnStatementAs = "Test"
            };

            var debitReceived = debitService.Create(debitSent,bankAccount.BankAccounts[0]);

            var refundSent = new Refund
            {
                Amount = 500,
                Description = "Test Refund",
            };

            var refundReceived = refundService.Create(refundSent, debitReceived.Debits[0]);

            Assert.IsNotNull(refundReceived);
            Assert.IsNotNull(refundReceived.Refunds);
            Assert.IsTrue(refundReceived.Refunds.Count > 0);
            Assert.IsNotNull(refundReceived.Refunds[0].Id);
            Assert.IsTrue(refundReceived.Refunds[0].Amount == refundSent.Amount);
            Assert.IsTrue(string.Compare(refundReceived.Refunds[0].Description, refundSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Refund()
        {
            var refundService = new RefundService();
            var refund = refundService.Get(new Refund { Id = BalancedTestKeys.RefundTestId });

            Assert.IsNotNull(refund);
            Assert.IsNotNull(refund.Refunds);
            Assert.IsTrue(refund.Refunds.Count > 0);
            Assert.IsTrue(refund.Refunds[0].Id == BalancedTestKeys.RefundTestId);
        }

        [TestMethod]
        public void List_Refunds()
        {
            var refundService = new RefundService();
            var refund = refundService.List();

            Assert.IsNotNull(refund);
            Assert.IsNotNull(refund.Refunds);
            Assert.IsTrue(refund.Refunds.Count > 0);
        }

        [TestMethod]
        public void Update_Refund()
        {
            var refundService = new RefundService();

            var refundReceived = refundService.Get(new Refund {Id = BalancedTestKeys.RefundTestId});

            refundReceived.Refunds[0].Description = "Updated Description";
            refundReceived.Refunds[0].Meta = new Dictionary<string, string>
            {
                {"testkey", "testvalue"}
            };

            var refundUpdated = refundService.Update(refundReceived.Refunds[0]);

            Assert.IsNotNull(refundUpdated);
            Assert.IsNotNull(refundUpdated.Refunds);
            Assert.IsTrue(refundUpdated.Refunds.Count > 0);
            Assert.IsTrue(refundReceived.Refunds[0].Amount == refundUpdated.Refunds[0].Amount);
            Assert.IsTrue(string.Compare(refundReceived.Refunds[0].Id, refundUpdated.Refunds[0].Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(refundReceived.Refunds[0].Description, refundUpdated.Refunds[0].Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(refundUpdated.Refunds[0].Meta);
            Assert.IsTrue(refundUpdated.Refunds[0].Meta.Count > 0);
            Assert.IsNotNull(refundUpdated.Refunds[0].Meta["testkey"]);
            Assert.IsTrue(string.Compare(refundUpdated.Refunds[0].Meta["testkey"], refundReceived.Refunds[0].Meta["testkey"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
