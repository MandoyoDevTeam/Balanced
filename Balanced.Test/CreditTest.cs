using System;
using System.Linq;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Balanced.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class CreditTest
    {
         public Marketplace Marketplace { get; private set; }

         public CreditTest()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            Marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });
        }

        [TestMethod]
         public void Connect_Credit_Rest()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var items = creditService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Credit_Rest_Fake()
        {
            var creditService = new CreditService(BalancedSettings.FakeSecret, Marketplace);
            //should throws an exception
            creditService.List();
        }

        [TestMethod]
        public void Create_Credit_For_New_BankAccount()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);

            var creditSent = new Credit
            {
                BankAccount = new BankAccount
                                {
                                    Name = "Mandoyo Inc",
                                    AccountNumber = "9900000001",
                                    RoutingNumber = "121000358",
                                    Type = BankAccountType.Checking
                                },
                Amount = 1000,
                Description = "Test Credit",
                AppearsOnStatementAs = "Test"
            };

            var creditReceived = creditService.CreateForNewBankAccount(creditSent);

            Assert.IsNotNull(creditReceived);
            Assert.IsNotNull(creditReceived.BankAccount);
            Assert.IsNotNull(creditReceived.Id);
            Assert.IsTrue(creditReceived.Amount == creditSent.Amount);
            Assert.IsTrue(string.Compare(creditReceived.Description, creditSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(creditReceived.AppearsOnStatementAs, creditSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Create_Credit_For_Existing_BankAccount()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);

            var creditSent = new Credit
            {
                BankAccount = bankAccountService.Get(new BankAccount {Id = BalancedSettings.BankAccountTestId}),
                Amount = 1000,
                Description = "Test Credit",
                AppearsOnStatementAs = "Test"
            };

            var creditReceived = creditService.CreateForExistingBankAccount(creditSent);

            Assert.IsNotNull(creditReceived);
            Assert.IsNotNull(creditReceived.BankAccount);
            Assert.IsNotNull(creditReceived.Id);
            Assert.IsTrue(creditReceived.Amount == creditSent.Amount);
            Assert.IsTrue(string.Compare(creditReceived.Description, creditSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(creditReceived.AppearsOnStatementAs, creditSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        [TestMethod]
        public void Create_Credit_For_Existing_Customer()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var creditSent = new Credit
            {
                Customer = customerService.Get(new Customer { Id = BalancedSettings.CustomerTestId }),
                Amount = 1000,
                Description = "Test Credit",
                AppearsOnStatementAs = "Test"
            };
            
            var creditReceived = creditService.CreateForExistingCustomer(creditSent);

            Assert.IsNotNull(creditReceived);
            Assert.IsNotNull(creditReceived.Customer);
            Assert.IsNotNull(creditReceived.Id);
            Assert.IsTrue(creditReceived.Amount == creditSent.Amount);
            Assert.IsTrue(string.Compare(creditReceived.Description, creditSent.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(string.Compare(creditReceived.AppearsOnStatementAs, creditSent.AppearsOnStatementAs, StringComparison.InvariantCultureIgnoreCase) == 0);
        }


        [TestMethod]
        public void Get_Credit()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var credit = creditService.Get(new Credit { Id = BalancedSettings.CreditTestId });

            Assert.IsNotNull(credit);
            Assert.IsTrue(credit.Id == BalancedSettings.CreditTestId);
        }

        [TestMethod]
        public void List_Credits()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var items = creditService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void List_BankAccount_Credits()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);

            var bankAccount = bankAccountService.Get(new BankAccount {Id = BalancedSettings.BankAccountTestId });

            var items = creditService.ListForBankAccount(bankAccount);

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count >= 0);
        }

        [TestMethod]
        public void List_Customer_Credits()
        {
            var creditService = new CreditService(BalancedSettings.Secret, Marketplace);
            var customerService = new CustomerService(BalancedSettings.Secret);

            var customer = customerService.Get(new Customer {Id = BalancedSettings.CustomerTestId });

            var items = creditService.ListForCustomer(customer);

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }
    }
}
