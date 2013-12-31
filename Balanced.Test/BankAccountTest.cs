using System;
using Balanced.Structs;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class BankAccountTest
    {
        [TestMethod]
        public void Connect_Bank_Account_Rest()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var items = bankAccountService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Bank_Account_Rest_Fake()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.FakeSecret);
            //should throws an exception
            bankAccountService.List();
        }

        [TestMethod]
        public void Create_Bank_Account()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            }; 
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            Assert.IsNotNull(bankAccountReceived);
            Assert.IsNotNull(bankAccountReceived.Id);
            Assert.IsTrue(String.Compare(bankAccountReceived.Name,bankAccountSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.AccountNumber, bankAccountSent.AccountNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.RoutingNumber, bankAccountSent.RoutingNumber, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(bankAccountReceived.Type == bankAccountSent.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Bank_Account_Missing_Required_Field()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                //AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            bankAccountService.Create(bankAccountSent);

        }

        [TestMethod]
        public void Create_Bank_Account_Bad_Bank_Name()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                BankName = "Fake Bank Name",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            Assert.IsNotNull(bankAccountReceived);
            Assert.IsNotNull(bankAccountReceived.Id);
            Assert.IsTrue(String.Compare(bankAccountReceived.Name, bankAccountSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankName, bankAccountSent.BankName, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.AccountNumber, bankAccountSent.AccountNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.RoutingNumber, bankAccountSent.RoutingNumber, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(bankAccountReceived.Type == bankAccountSent.Type);
        }

        
        [TestMethod]
        public void Get_Bank_Account()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccount = bankAccountService.Get(new BankAccount { Id = BalancedSettings.BankAccountTestId });

            Assert.IsNotNull(bankAccount);
            Assert.IsTrue(bankAccount.Id == BalancedSettings.BankAccountTestId);
        }

        [TestMethod]
        public void List_Bank_Account()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var items = bankAccountService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }

        [TestMethod]
        public void Delete_Bank_Account()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var result = bankAccountService.Delete(bankAccountReceived);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Bank_Account()
        {

            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Id = "Mandoyo_Inc_Fake",
            };
            bankAccountService.Delete(bankAccountSent);
        
        }   
     
    }
}