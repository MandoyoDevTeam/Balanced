using System;
using System.Collections.Generic;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class RefundTest
    {
        public Marketplace Marketplace { get; private set; }

        public RefundTest()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            Marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });
        }

        [TestMethod]
         public void Connect_Refund_Rest()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);
            var items = refundService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Refund_Rest_Fake()
        {
            var refundService = new RefundService(BalancedSettings.FakeSecret, Marketplace);
            //should throws an exception
            refundService.List();
        }

        [TestMethod]
        public void Create_Refund()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var debitSent = new Debit
            {
                Customer = customerService.Get(new Customer { Id = BalancedSettings.CustomerTestId }),
                Amount = 1000,
                Description = "Test Debit",
                AppearsOnStatementAs = "Test"
            };
            debitSent.CustomerUri = debitSent.Customer.Uri;

            var debitReceived = debitService.Create(debitSent);

            var refundSent = new Refund
            {
                Customer = debitReceived.Customer,
                Amount = 500,
                Description = "Test Refund",
                AppearsOnStatementAs = "Test",
                DebitUri = debitReceived.Uri
            };

            var refundReceived = refundService.Create(refundSent);

            Assert.IsNotNull(refundReceived);
            Assert.IsNotNull(refundReceived.Customer);
            Assert.IsNotNull(refundReceived.Debit);
            Assert.IsNotNull(refundReceived.Id);
            Assert.IsTrue(refundReceived.Amount == refundSent.Amount);
            Assert.IsTrue(string.Compare(refundReceived.Description, refundSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Refund()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);
            var refund = refundService.Get(new Refund { Id = BalancedSettings.RefundTestId });

            Assert.IsNotNull(refund);
            Assert.IsTrue(refund.Id == BalancedSettings.RefundTestId);
        }

        [TestMethod]
        public void List_Refunds()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);
            var items = refundService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void List_Customer_Refunds()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var customer = customerService.Get(new Customer {Id = BalancedSettings.CustomerTestId});

            var items = refundService.ListForCustomer(customer);

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Update_Refund()
        {
            var refundService = new RefundService(BalancedSettings.Secret, Marketplace);

            var refundReceived = refundService.Get(new Refund {Id = BalancedSettings.RefundTestId});

            refundReceived.Description = "Updated Description";
            refundReceived.Meta = new Dictionary<string, string>
            {
                {"testkey", "testvalue"}
            };

            var refundUpdated = refundService.Update(refundReceived);

            Assert.IsNotNull(refundUpdated);
            Assert.IsTrue(refundReceived.Amount == refundUpdated.Amount);
            Assert.IsTrue(string.Compare(refundReceived.Id, refundUpdated.Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(refundReceived.Description, refundUpdated.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(refundUpdated.Meta);
            Assert.IsTrue(refundUpdated.Meta.Count > 0);
            Assert.IsNotNull(refundUpdated.Meta["testkey"]);
            Assert.IsTrue(string.Compare(refundUpdated.Meta["testkey"], refundReceived.Meta["testkey"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
