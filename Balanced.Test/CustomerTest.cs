using System;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Balanced.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void Connect_Customer_Rest()
        {
            var bankAccountService = new CustomerService(BalancedSettings.Secret);
            var items = bankAccountService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Customer_Rest_Fake()
        {
            var bankAccountService = new CustomerService(BalancedSettings.FakeSecret);
            //should throws an exception
            bankAccountService.List();
        }

        [TestMethod]
        public void Create_Empty_Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var customerSent = new Customer();
            
            var customerReceived = customerService.Create(customerSent);

            Assert.IsNotNull(customerReceived);
            Assert.IsNotNull(customerReceived.Id);
        }

        [TestMethod]
        public void Create_Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var customerSent = new Customer
            {
                Name = "Mandoyo Inc",
                SSNLast4 = "4977",
                DateOfBirth = DateTime.Parse("15/03/1981") ,
                Email = "cto-office@mandoyo.com",
                Phone = "+34 667123456",
                BusinessName = "Mandoyo",
                Ein = "030089800",
                Address = new Address
                {
                    City = "New York",
                    CountryCode = "USA",
                    PostalCode = "90210",
                    State = "New York",
                    Line1 = "Fake Street",
                }
            };

            var customerReceived = customerService.Create(customerSent);

            Assert.IsNotNull(customerReceived);
            Assert.IsNotNull(customerReceived.Id);
            
            Assert.IsNotNull(customerReceived.Address);
            Assert.IsTrue(string.Compare(customerSent.Address.State, customerReceived.Address.State, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.City, customerReceived.Address.City, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.CountryCode, customerReceived.Address.CountryCode, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.PostalCode, customerReceived.Address.PostalCode, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.Line1, customerReceived.Address.Line1, StringComparison.InvariantCultureIgnoreCase) == 0);

            Assert.IsTrue(string.Compare(customerSent.Name, customerReceived.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.SSNLast4, customerReceived.SSNLast4, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(string.Compare("xxxx", customerReceived.SSNLast4, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(customerReceived.DateOfBirth.HasValue);
            Assert.IsTrue(string.Compare(string.Format("{0:yyyy-MM}", customerSent.DateOfBirth.Value), string.Format("{0:yyyy-MM}", customerReceived.DateOfBirth.Value), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var customer = customerService.Get(new Customer { Id = BalancedSettings.CustomerTestId });

            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.Id == BalancedSettings.CustomerTestId);
        }

        [TestMethod]
        public void List_Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var items = customerService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Add_Card_To_Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            var marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });

            var cardService = new CardService(BalancedSettings.Secret, marketplace);

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

            var customerSent = new Customer
            {
                Name = "Mandoyo Inc",
                SSNLast4 = "4977",
                DateOfBirth = DateTime.Parse("15/03/1981"),
                Email = "cto-office@mandoyo.com",
                Phone = "+34 667123456",
                BusinessName = "Mandoyo",
                Ein = "030089800"
            };

            var customerReceived = customerService.Create(customerSent);

            var customer = customerService.AddCard(customerReceived, cardReceived);

            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void Add_BankAccount_To__Customer()
        {
            var customerService = new CustomerService(BalancedSettings.Secret);
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);

            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var customerSent = new Customer
            {
                Name = "Mandoyo Inc",
                SSNLast4 = "4977",
                DateOfBirth = DateTime.Parse("15/03/1981"),
                Email = "cto-office@mandoyo.com",
                Phone = "+34 667123456",
                BusinessName = "Mandoyo",
                Ein = "030089800"
            };

            var customerReceived = customerService.Create(customerSent);

            var customer = customerService.AddBankAccount(customerReceived, bankAccountReceived);

            Assert.IsNotNull(customer);
            
        }

    }
}
