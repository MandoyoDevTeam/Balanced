using System;
using System.Collections.Generic;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class CardTest
    {

        public Marketplace Marketplace { get; private set; }

        public CardTest()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            Marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });
        }

        [TestMethod]
        public void Connect_Card_Rest()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var items = cardService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Card_Rest_Fake()
        {
            var cardService = new CardService(BalancedSettings.FakeSecret, Marketplace);
            //should throws an exception
            cardService.List();
        }

        [TestMethod]
        public void Create_Fully_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                City ="New York",
                PostalCode = "10005",
                StreetAddress = "140 Broadway",
                CountryCode = "USA",
                IsValid = true
            }; 
            var cardReceived = cardService.Create(cardSent);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Id);
            Assert.IsTrue(String.Compare(cardReceived.Name,cardSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(cardReceived.ExpirationYear == cardSent.ExpirationYear);
            Assert.IsTrue(cardReceived.ExpirationMonth == cardSent.ExpirationMonth);
            Assert.IsTrue(String.Compare(cardReceived.CardNumber, cardSent.CardNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(cardReceived.IsVerified.HasValue && cardReceived.IsVerified.Value);
        }

        [TestMethod]
        public void Create_Required_Fields_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
            };
            var cardReceived = cardService.Create(cardSent);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Id);
            Assert.IsTrue(cardReceived.ExpirationYear == cardSent.ExpirationYear);
            Assert.IsTrue(cardReceived.ExpirationMonth == cardSent.ExpirationMonth);
            Assert.IsTrue(String.Compare(cardReceived.CardNumber, cardSent.CardNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(cardReceived.IsVerified.HasValue && !cardReceived.IsVerified.Value);
        }

        
        [TestMethod]
        public void Get_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var marketplace = cardService.Get(new Card { Id = BalancedSettings.CardTestId });

            Assert.IsNotNull(marketplace);
            Assert.IsTrue(marketplace.Id == BalancedSettings.CardTestId);
        }

        [TestMethod]
        public void List_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var items = cardService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Update_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                City = "New York",
                PostalCode = "10005",
                StreetAddress = "140 Broadway",
                CountryCode = "USA",
                IsValid = true
            };
            var cardReceived = cardService.Create(cardSent);

            cardReceived.Meta = new Dictionary<string, string> { { "facebooklink", "linktofacebook" }, { "twitterlink", "linktotwitter" } };

            cardReceived = cardService.Update(cardReceived);

            Assert.IsNotNull(cardReceived);
            Assert.IsNotNull(cardReceived.Meta);
            Assert.IsTrue(cardReceived.Meta.Count == 2);
            Assert.IsNotNull(cardReceived.Meta["facebooklink"]);
            Assert.IsNotNull(cardReceived.Meta["twitterlink"]);
        }

        [TestMethod]
        public void Invalidate_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                City = "New York",
                PostalCode = "10005",
                StreetAddress = "140 Broadway",
                CountryCode = "USA",
                IsValid = true
            };
            var cardReceived = cardService.Create(cardSent);

            cardReceived = cardService.Invalidate(cardReceived);

            Assert.IsNotNull(cardReceived);
            Assert.IsFalse(cardReceived.IsValid);
        }

        [TestMethod]
        public void Delete_Card()
        {
            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                CardNumber = "5105105105105100",
                ExpirationYear = 2020,
                ExpirationMonth = 8,
                Name = "Mandoyo Inc",
                SecurityCode = "123",
                PhoneNumber = "666123456",
                City = "New York",
                PostalCode = "10005",
                StreetAddress = "140 Broadway",
                CountryCode = "USA",
                IsValid = true
            }; 
            var cardReceived = cardService.Create(cardSent);

            var result = cardService.Delete(cardReceived);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Card()
        {

            var cardService = new CardService(BalancedSettings.Secret, Marketplace);
            var cardSent = new Card
            {
                Id = "Mandoyo_Inc_Fake",
            };
            cardService.Delete(cardSent);
        
        }   
     
    }
}