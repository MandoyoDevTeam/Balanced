using System;
using Balanced.Config;
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

        public CustomerTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
        public void Connect_Customer_Rest()
        {
            var bankAccountService = new CustomerService();
            var items = bankAccountService.List();

            Assert.IsNotNull(items);

        }

       [TestMethod]
        public void Create_Empty_Customer()
        {
            var customerService = new CustomerService();
            var customerSent = new Customer();
            
            var customerReceived = customerService.Create(customerSent);

            Assert.IsNotNull(customerReceived);
            Assert.IsNotNull(customerReceived.Customers);
            Assert.IsTrue(customerReceived.Customers.Count > 0);
            Assert.IsNotNull(customerReceived.Customers[0].Id);
        }

        [TestMethod]
        public void Create_Customer()
        {
            var customerService = new CustomerService();
            var customerSent = new Customer
            {
                Name = "Mandoyo Inc",
                SSNLast4 = "4977",
                DobYear = "1981",
                DobMonth = "03",
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
            Assert.IsNotNull(customerReceived.Customers);
            Assert.IsTrue(customerReceived.Customers.Count > 0);
            Assert.IsNotNull(customerReceived.Customers[0].Id);

            Assert.IsNotNull(customerReceived.Customers[0].Address);
            Assert.IsTrue(string.Compare(customerSent.Address.State, customerReceived.Customers[0].Address.State, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.City, customerReceived.Customers[0].Address.City, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.CountryCode, customerReceived.Customers[0].Address.CountryCode, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.PostalCode, customerReceived.Customers[0].Address.PostalCode, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.Address.Line1, customerReceived.Customers[0].Address.Line1, StringComparison.InvariantCultureIgnoreCase) == 0);

            Assert.IsTrue(string.Compare(customerSent.Name, customerReceived.Customers[0].Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(customerSent.SSNLast4, customerReceived.Customers[0].SSNLast4, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(string.Compare("xxxx", customerReceived.Customers[0].SSNLast4, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(string.Format("{0}-{1}", customerSent.DobYear, customerSent.DobMonth), string.Format("{0}-{1}", customerReceived.Customers[0].DobYear, customerReceived.Customers[0].DobMonth), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Get_Customer()
        {
            var customerService = new CustomerService();
            var customer = customerService.Get(new Customer { Id = BalancedTestKeys.CustomerTestId });

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Customers);
            Assert.IsTrue(customer.Customers.Count > 0);
            Assert.IsTrue(customer.Customers[0].Id == BalancedTestKeys.CustomerTestId);
        }

        [TestMethod]
        public void List_Customer()
        {
            var customerService = new CustomerService();
            var customer = customerService.List();

            Assert.IsNotNull(customer);
            Assert.IsNotNull(customer.Customers);
            Assert.IsTrue(customer.Customers.Count > 0);
        }

        [TestMethod]
        public void Add_Card_To_Customer()
        {
            var customerService = new CustomerService();

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

            var customerSent = new Customer
            {
                Name = "Mandoyo Inc",
                SSNLast4 = "4977",
                DobYear = "1981",
                DobMonth = "03",
                Email = "cto-office@mandoyo.com",
                Phone = "+34 667123456",
                BusinessName = "Mandoyo",
                Ein = "030089800"
            };

            var customerReceived = customerService.Create(customerSent);

            var customer = customerService.AssociateCard(customerReceived.Customers[0], cardReceived.Cards[0]);

            Assert.IsNotNull(customer);
        }

        [TestMethod]
        public void Add_BankAccount_To__Customer()
        {
            var customerService = new CustomerService();
            var bankAccountService = new BankAccountService();

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
                DobYear = "1981",
                DobMonth = "03",
                Email = "cto-office@mandoyo.com",
                Phone = "+34 667123456",
                BusinessName = "Mandoyo",
                Ein = "030089800"
            };

            var customerReceived = customerService.Create(customerSent);

            var customer = customerService.AssociateBankAccount(customerReceived.Customers[0], bankAccountReceived.BankAccounts[0]);

            Assert.IsNotNull(customer);
            
        }

    }
}