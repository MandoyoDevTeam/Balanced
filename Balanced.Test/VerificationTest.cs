using Balanced.Config;
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

        public VerificationTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }
        [TestMethod]
        public void Connect_Verification_Rest()
        {
            var verificationService = new VerificationService();
            var items = verificationService.List();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Create_Verification()
        {
            var verificationService = new VerificationService();
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

            customerService.AssociateBankAccount(customerReceived.Customers[0], bankAccountReceived.BankAccounts[0]);

            var verification = verificationService.Create(bankAccountReceived.BankAccounts[0]);

            Assert.IsNotNull(verification);
            Assert.IsNotNull(verification.Verifications);
            Assert.IsTrue(verification.Verifications.Count > 0);
            Assert.IsNotNull(verification.Verifications[0].Href);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Create_Verification_WithOut_Customer()
        {
            var verificationService = new VerificationService();
            var bankAccountService = new BankAccountService();
            var bankAccountSent = new BankAccount
            {
                Name = "Mandoyo Inc",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };
            var bankAccountReceived = bankAccountService.Create(bankAccountSent);

            var verification = verificationService.Create(bankAccountReceived.BankAccounts[0]);

            Assert.IsNotNull(verification);
            Assert.IsNotNull(verification.Verifications);
            Assert.IsTrue(verification.Verifications.Count > 0);
            Assert.IsNotNull(verification.Verifications[0].Href);
        }

        [TestMethod]
        public void Get_Verification()
        {
            var verificationService = new VerificationService();

            var verification = verificationService.Get(new Verification { Id = BalancedTestKeys.VerificationTestId });

            Assert.IsNotNull(verification);
            Assert.IsNotNull(verification.Verifications);
            Assert.IsTrue(verification.Verifications.Count > 0);
            Assert.IsTrue(verification.Verifications[0].Id == BalancedTestKeys.VerificationTestId);
        }

        [TestMethod]
        public void List_Verifications()
        {
            var verificationService = new VerificationService();

            var verifications = verificationService.List();

            Assert.IsNotNull(verifications);
        }

        [TestMethod]
        public void Confirm_Verification()
        {
            var verificationService = new VerificationService();

            var verification = verificationService.Get(new Verification { Id = BalancedTestKeys.VerificationTestId });

            var confirmedVerification = verificationService.Confirm(verification.Verifications[0], 1, 1);

            Assert.IsNotNull(confirmedVerification);
            Assert.IsNotNull(confirmedVerification.Verifications);
            Assert.IsTrue(confirmedVerification.Verifications.Count > 0);
        }
    }
}
