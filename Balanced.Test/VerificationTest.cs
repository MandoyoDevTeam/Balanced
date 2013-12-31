using System;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Balanced.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class VerificationTest
    {

        public BankAccount BankAccount { get; set; }

        public VerificationTest()
        {
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            BankAccount = bankAccountService.Get(new BankAccount { Id = BalancedSettings.BankAccountTestId });
        }
        [TestMethod]
        public void Connect_Verification_Rest()
        {
            var verificationService = new VerificationService(BalancedSettings.Secret);
            var items = verificationService.List(BankAccount);

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_Bank_Account_Rest_Fake()
        {
            var verificationService = new VerificationService(BalancedSettings.FakeSecret);
            //should throws an exception
            verificationService.List(BankAccount);
        }

        [TestMethod]
        public void Create_Verification()
        {
            var verificationService = new VerificationService(BalancedSettings.Secret);
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

            customerService.AddBankAccount(customerReceived, bankAccountReceived);

            var verification = verificationService.Create(bankAccountReceived);

            Assert.IsNotNull(verification);
            Assert.IsNotNull(verification.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Create_Verification_WithOut_Customer()
        {
            var verificationService = new VerificationService(BalancedSettings.Secret);
            var bankAccountService = new BankAccountService(BalancedSettings.Secret);
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var verification = verificationService.Create(bankAccountReceived);

            Assert.IsNotNull(verification);
            Assert.IsNotNull(verification.Id);
        }

        [TestMethod]
        public void Get_Verification()
        {
            var verificationService = new VerificationService(BalancedSettings.Secret);

            var verification = verificationService.Get(BankAccount, new Verification { Id = BalancedSettings.VerificationTestId });

            Assert.IsNotNull(verification);
            Assert.IsTrue(verification.Id == BalancedSettings.VerificationTestId);
        }

        [TestMethod]
        public void List_Verifications()
        {
            var verificationService = new VerificationService(BalancedSettings.Secret);

            var verifications = verificationService.List(BankAccount);

            Assert.IsNotNull(verifications);
        }
    }
}
