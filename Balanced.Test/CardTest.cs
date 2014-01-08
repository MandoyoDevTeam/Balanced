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
    public class CardTest
    {

        public CardTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
        public void Connect_Card_Rest()
        {
            var cardService = new CardService();
            var items = cardService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        public void Create_Fully_Card()
        {
            var cardService = new CardService();
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                Address = new Address
                {
                    City ="New York",
                    PostalCode = "10005",
                    Line1 = "140 Broadway",
                    CountryCode = "USA",
                },
                Verify = true
            }; 
            var cardReceived = cardService.Create(cardSent);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Cards);
            Assert.IsTrue(cardReceived.Cards.Count > 0);

            Assert.IsNotNull(cardReceived.Cards[0].Id);
            Assert.IsTrue(String.Compare(cardReceived.Cards[0].Name, cardSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(cardReceived.Cards[0].ExpirationYear == cardSent.ExpirationYear);
            Assert.IsTrue(cardReceived.Cards[0].ExpirationMonth == cardSent.ExpirationMonth);
            Assert.IsTrue(String.Compare(cardReceived.Cards[0].Number, cardSent.Number, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(cardReceived.Cards[0].IsVerified.HasValue && cardReceived.Cards[0].IsVerified.Value);
        }

        [TestMethod]
        public void Create_Required_Fields_Card()
        {
            var cardService = new CardService();
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
            };
            var cardReceived = cardService.Create(cardSent);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Cards);
            Assert.IsTrue(cardReceived.Cards.Count > 0);

            Assert.IsNotNull(cardReceived.Cards[0].Id);
            Assert.IsTrue(cardReceived.Cards[0].ExpirationYear == cardSent.ExpirationYear);
            Assert.IsTrue(cardReceived.Cards[0].ExpirationMonth == cardSent.ExpirationMonth);
            Assert.IsTrue(String.Compare(cardReceived.Cards[0].Number, cardSent.Number, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(cardReceived.Cards[0].IsVerified.HasValue && !cardReceived.Cards[0].IsVerified.Value);
        }

        
        [TestMethod]
        public void Get_Card()
        {
            var cardService = new CardService();
            var card = cardService.Get(new Card { Id = BalancedTestKeys.CardTestId });

            Assert.IsNotNull(card);
            Assert.IsNotNull(card.Cards);
            Assert.IsTrue(card.Cards.Count > 0);
            Assert.IsTrue(card.Cards[0].Id == BalancedTestKeys.CardTestId);
        }

        [TestMethod]
        public void List_Card()
        {
            var cardService = new CardService();
            var card = cardService.List();

            Assert.IsNotNull(card);
            Assert.IsNotNull(card.Cards);
            Assert.IsTrue(card.Cards.Count > 0);
        }

        [TestMethod]
        public void Update_Card()
        {
            var cardService = new CardService();
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                Address = new Address
                {
                    City = "New York",
                    PostalCode = "10005",
                    Line1 = "140 Broadway",
                    CountryCode = "USA",
                }
            };
            var cardReceived = cardService.Create(cardSent);

            cardReceived.Cards[0].Meta = new Dictionary<string, string> { { "facebooklink", "linktofacebook" }, { "twitterlink", "linktotwitter" } };

            cardReceived = cardService.Update(cardReceived.Cards[0]);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Cards);
            Assert.IsTrue(cardReceived.Cards.Count > 0);
            Assert.IsNotNull(cardReceived.Cards[0].Meta);
            Assert.IsTrue(cardReceived.Cards[0].Meta.Count == 2);
            Assert.IsNotNull(cardReceived.Cards[0].Meta["facebooklink"]);
            Assert.IsNotNull(cardReceived.Cards[0].Meta["twitterlink"]);
        }

        [TestMethod]
        public void Delete_Card()
        {
            var cardService = new CardService();
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                Address = new Address
                {
                    City = "New York",
                    PostalCode = "10005",
                    Line1 = "140 Broadway",
                    CountryCode = "USA",
                },
                Verify = true
            };
            var cardReceived = cardService.Create(cardSent);

            var isDeleted = cardService.Delete(cardReceived.Cards[0]);

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Card()
        {

            var cardService = new CardService();
            var cardSent = new Card
            {
                Id = "Mandoyo_Inc_Fake",
            };
            cardService.Delete(cardSent);
        
        }   
     
    }
}