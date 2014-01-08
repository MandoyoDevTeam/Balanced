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
    public class HoldTest
    {

        public HoldTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
         public void Connect_Hold_Rest()
        {
            var holdService = new HoldService();
            var items = holdService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        public void Create_Hold()
        {
            var holdService = new HoldService();
            var cardService = new CardService();
            
            var cardSent = cardService.Get(new Card {Id = BalancedTestKeys.CardTestId});
            var holdSent = new Hold
            {
                Amount = 1000,
                Description = "Test Hold",
                AppearsOnStatementAs = "Test",
            };

            var holdReceived = holdService.Create(holdSent, cardSent.Cards[0]);

            Assert.IsNotNull(holdReceived);
            Assert.IsTrue(holdReceived.Holds.Count > 0);
            Assert.IsFalse(string.IsNullOrEmpty(holdReceived.Holds[0].TransactionNumber));
            Assert.IsNotNull(holdReceived.Holds[0].Id);
            Assert.IsTrue(holdReceived.Holds[0].Amount == holdSent.Amount);
            Assert.IsTrue(string.Compare(holdReceived.Holds[0].Description, holdSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Hold()
        {
            var holdService = new HoldService();
            var hold = holdService.Get(new Hold { Id = BalancedTestKeys.HoldTestId });

            Assert.IsNotNull(hold);
            Assert.IsTrue(hold.Holds.Count > 0);
            Assert.IsTrue(hold.Holds[0].Id == BalancedTestKeys.HoldTestId);
        }

        [TestMethod]
        public void List_Holds()
        {
            var holdService = new HoldService();
            var items = holdService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Holds);
            Assert.IsTrue(items.Holds.Count > 0);
        }

     
        [TestMethod]
        public void Update_Hold()
        {
            var holdService = new HoldService();

            var holdReceived = holdService.Get(new Hold {Id = BalancedTestKeys.HoldTestId});

            holdReceived.Holds[0].Description = "Updated Description";
            holdReceived.Holds[0].Meta = new Dictionary<string, string>
            {
                {"testkey", "testvalue"}
            };

            var holdUpdated = holdService.Update(holdReceived.Holds[0]);

            Assert.IsNotNull(holdUpdated);
            Assert.IsNotNull(holdUpdated.Holds);
            Assert.IsTrue(holdUpdated.Holds.Count > 0);
            Assert.IsTrue(holdReceived.Holds[0].Amount == holdUpdated.Holds[0].Amount);
            Assert.IsTrue(string.Compare(holdReceived.Holds[0].Id, holdUpdated.Holds[0].Id, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(holdReceived.Holds[0].Description, holdUpdated.Holds[0].Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsNotNull(holdUpdated.Holds[0].Meta);
            Assert.IsTrue(holdUpdated.Holds[0].Meta.Count > 0);
            Assert.IsNotNull(holdUpdated.Holds[0].Meta["testkey"]);
            Assert.IsTrue(string.Compare(holdUpdated.Holds[0].Meta["testkey"], holdReceived.Holds[0].Meta["testkey"], StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Capture_Hold()
        {
            var holdService = new HoldService();
            var holdReceived = holdService.Get(new Hold { Id = BalancedTestKeys.HoldTestId });

            var debitCaptured = holdService.Capture(holdReceived.Holds[0]);

            Assert.IsNotNull(debitCaptured);
            Assert.IsTrue(debitCaptured.Debits.Count > 0);
            Assert.IsFalse(string.IsNullOrEmpty(debitCaptured.Debits[0].TransactionNumber));
        }

        [TestMethod]
        public void Void_Hold()
        {
            var holdService = new HoldService();
            var cardService = new CardService();

            var cardSent = cardService.Get(new Card { Id = BalancedTestKeys.CardTestId });
            var holdSent = new Hold
            {
                Amount = 1000,
                Description = "Test Hold",
                AppearsOnStatementAs = "Test",
            };

            var holdReceived = holdService.Create(holdSent, cardSent.Cards[0]);

            var holdVoid = holdService.Void(holdReceived.Holds[0]);

            Assert.IsNotNull(holdVoid);
            Assert.IsNotNull(holdVoid.Holds);
            Assert.IsTrue(holdVoid.Holds.Count > 0);
            Assert.IsTrue(holdVoid.Holds[0].IsVoid);

        }
    }
}
