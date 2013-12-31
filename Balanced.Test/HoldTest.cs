using System;
using System.Collections.Generic;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class HoldTest
    {
        public Marketplace Marketplace { get; private set; }

        public HoldTest()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            Marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });
        }

        [TestMethod]
         public void Connect_Hold_Rest()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var items = holdService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Hold_Rest_Fake()
        {
            var holdService = new HoldService(BalancedSettings.FakeSecret, Marketplace);
            //should throws an exception
            holdService.List();
        }

        [TestMethod]
        public void Create_Hold()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);

            var holdSent = new Hold
            {
                Customer = customerService.Get(new Customer {Id = BalancedSettings.CustomerTestId}),
                Amount = 1000,
                Description = "Test Hold",
                AppearsOnStatementAs = "Test",
                SourceUri = cardService.Get(new Card {Id = BalancedSettings.CardTestId}).Uri,
            };

            var holdReceived = holdService.Create(holdSent);

            Assert.IsNotNull(holdReceived);
            Assert.IsNotNull(holdReceived.Customer);
            Assert.IsNotNull(holdReceived.Source);
            Assert.IsFalse(string.IsNullOrEmpty(holdReceived.TransactionNumber));
            Assert.IsNotNull(holdReceived.Id);
            Assert.IsTrue(holdReceived.Amount == holdSent.Amount);
            Assert.IsTrue(string.Compare(holdReceived.Description, holdSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Hold()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var hold = holdService.Get(new Hold { Id = BalancedSettings.HoldTestId });

            Assert.IsNotNull(hold);
            Assert.IsTrue(hold.Id == BalancedSettings.HoldTestId);
        }

        [TestMethod]
        public void List_Holds()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var items = holdService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void List_Customer_Holds()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var customer = customerService.Get(new Customer {Id = BalancedSettings.CustomerTestId});

            var items = holdService.ListForCustomer(customer);

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Update_Hold()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);

            var holdReceived = holdService.Get(new Hold {Id = BalancedSettings.HoldTestId});

            holdReceived.Description = "Updated Description";
            holdReceived.Meta = new Dictionary<string, string>
            {
                {"testkey", "testvalue"}
            };

            var holdUpdated = holdService.Update(holdReceived);

            Assert.IsNotNull(holdUpdated);
            Assert.IsTrue(holdReceived.Amount == holdUpdated.Amount);
            Assert.IsTrue(string.Compare(holdReceived.Id, holdUpdated.Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(holdReceived.Description, holdUpdated.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(holdUpdated.Meta);
            Assert.IsTrue(holdUpdated.Meta.Count > 0);
            Assert.IsNotNull(holdUpdated.Meta["testkey"]);
            Assert.IsTrue(string.Compare(holdUpdated.Meta["testkey"], holdReceived.Meta["testkey"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Capture_Hold()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var holdReceived = holdService.Get(new Hold { Id = BalancedSettings.HoldTestId });

            var debitCaptured = holdService.Capture(holdReceived);

            Assert.IsNotNull(debitCaptured);
            Assert.IsNotNull(debitCaptured.Customer);
            Assert.IsNotNull(debitCaptured.Source);
            Assert.IsFalse(string.IsNullOrEmpty(debitCaptured.TransactionNumber));
        }

        [TestMethod]
        public void Void_Hold()
        {
            var holdService = new HoldService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);

            var holdSent = new Hold
            {
                Customer = customerService.Get(new Customer { Id = BalancedSettings.CustomerTestId }),
                Amount = 1000,
                Description = "Test Hold",
                AppearsOnStatementAs = "Test",
                SourceUri = cardService.Get(new Card { Id = BalancedSettings.CardTestId }).Uri,
            };

            var holdReceived = holdService.Create(holdSent);

            var holdVoid = holdService.Void(holdReceived);

            Assert.IsNotNull(holdVoid);
            Assert.IsTrue(holdVoid.IsVoid);

        }
    }
}
