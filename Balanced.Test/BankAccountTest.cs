using System;
using Balanced.Config;
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
        public BankAccountTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
        public void Connect_Bank_Account_Rest()
        {
            var bankAccountService = new BankAccountService();
            var items = bankAccountService.List();

            Assert.IsNotNull(items);

        }        

        [TestMethod]
        public void Create_Bank_Account()
        {
            var bankAccountService = new BankAccountService();
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            }; 
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            Assert.IsNotNull(bankAccountReceived);
            Assert.IsNotNull(bankAccountReceived.BankAccounts);
            Assert.IsTrue(bankAccountReceived.BankAccounts.Count > 0);
            Assert.IsNotNull(bankAccountReceived.BankAccounts[0].Id);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].Name, bankAccountSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].AccountNumber, bankAccountSent.AccountNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].RoutingNumber, bankAccountSent.RoutingNumber, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(bankAccountReceived.BankAccounts[0].Type == bankAccountSent.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_Bank_Account_Missing_Required_Field()
        {
            var bankAccountService = new BankAccountService();
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
            var bankAccountService = new BankAccountService();
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
            Assert.IsNotNull(bankAccountReceived.BankAccounts);
            Assert.IsTrue(bankAccountReceived.BankAccounts.Count > 0);
            Assert.IsNotNull(bankAccountReceived.BankAccounts[0].Id);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].Name, bankAccountSent.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].BankName, bankAccountSent.BankName, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].AccountNumber, bankAccountSent.AccountNumber, StringComparison.InvariantCultureIgnoreCase) != 0);
            Assert.IsTrue(String.Compare(bankAccountReceived.BankAccounts[0].RoutingNumber, bankAccountSent.RoutingNumber, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(bankAccountReceived.BankAccounts[0].Type == bankAccountSent.Type);
        }

        
        [TestMethod]
        public void Get_Bank_Account()
        {
            var bankAccountService = new BankAccountService();
            var bankAccount = bankAccountService.Get(new BankAccount { Id = BalancedTestKeys.BankAccountTestId });

            Assert.IsNotNull(bankAccount);
            Assert.IsNotNull(bankAccount.BankAccounts);
            Assert.IsTrue(bankAccount.BankAccounts.Count > 0);
            Assert.IsTrue(bankAccount.BankAccounts[0].Id == BalancedTestKeys.BankAccountTestId);
        }

        [TestMethod]
        public void List_Bank_Account()
        {
            var bankAccountService = new BankAccountService();
            var items = bankAccountService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.BankAccounts);
            Assert.IsTrue(items.BankAccounts.Count > 0);
        }

        [TestMethod]
        public void Delete_Bank_Account()
        {
            var bankAccountService = new BankAccountService();
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var result = bankAccountService.Delete(bankAccountReceived.BankAccounts[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Bank_Account()
        {

            var bankAccountService = new BankAccountService();
            var bankAccountSent = new BankAccount
            {
                Id = "Mandoyo_Inc_Fake",
            };
            bankAccountService.Delete(bankAccountSent);
        
        }   
     
    }
}