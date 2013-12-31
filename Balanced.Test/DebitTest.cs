using System;
using System.Collections.Generic;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class DebitTest
    {
        public Marketplace Marketplace { get; private set; }

        public DebitTest()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            Marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });
        }

        [TestMethod]
         public void Connect_Debit_Rest()
        {
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);
            var items = debitService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Debit_Rest_Fake()
        {
            var debitService = new DebitService(BalancedSettings.FakeSecret, Marketplace);
            //should throws an exception
            debitService.List();
        }

        [TestMethod]
        public void Create_Debit()
        {
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

            Assert.IsNotNull(debitReceived);
            Assert.IsNotNull(debitReceived.Customer);
            Assert.IsNotNull(debitReceived.Id);
            Assert.IsTrue(debitReceived.Amount == debitSent.Amount);
            Assert.IsTrue(string.Compare(debitReceived.Description, debitSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitReceived.AppearsOnStatementAs, debitSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Debit()
        {
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);
            var debit = debitService.Get(new Debit { Id = BalancedSettings.DebitTestId });

            Assert.IsNotNull(debit);
            Assert.IsTrue(debit.Id == BalancedSettings.DebitTestId);
        }

        [TestMethod]
        public void List_Debits()
        {
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);
            var items = debitService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void List_Customer_Debits()
        {
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var customer = customerService.Get(new Customer {Id = BalancedSettings.CustomerTestId});

            var items = debitService.ListForCustomer(customer);

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Update_Debit()
        {
            var debitService = new DebitService(BalancedSettings.Secret, Marketplace);

            var debitReceived = debitService.Get(new Debit {Id = BalancedSettings.DebitTestId});

            debitReceived.Description = "Updated Description";
            debitReceived.Meta = new Dictionary<string, string>
            {
                {"testkey1", "testvalue1"},
                {"testkey2", "testvalue2"}
            };

            var debitUpdated = debitService.Update(debitReceived);

            Assert.IsNotNull(debitUpdated);
            Assert.IsTrue(debitReceived.Amount == debitUpdated.Amount);
            Assert.IsTrue(string.Compare(debitReceived.Id, debitUpdated.Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitReceived.Description, debitUpdated.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(debitUpdated.Meta);
            Assert.IsTrue(debitUpdated.Meta.Count > 0);
            Assert.IsNotNull(debitUpdated.Meta["testkey1"]);
            Assert.IsNotNull(debitUpdated.Meta["testkey2"]);
            Assert.IsTrue(string.Compare(debitUpdated.Meta["testkey1"], debitReceived.Meta["testkey1"], StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(debitUpdated.Meta["testkey2"], debitReceived.Meta["testkey2"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Refund_Debit()
        {
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

            var refund = debitService.Refund(debitReceived);

            Assert.IsNotNull(refund);

            Assert.IsTrue(refund.Amount == debitReceived.Amount);

        }
    }
}
