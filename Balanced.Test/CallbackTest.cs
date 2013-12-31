using System;
using Balanced.Structs;
using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class CallbackTest
    {

        [TestMethod]
        public void Connect_Success()
        {
            var callbackService = new CallbackService(BalancedSettings.Secret);
            var items = callbackService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_FakeSecret()
        {
            var callbackService = new CallbackService(BalancedSettings.FakeSecret);
            //should throws an exception
            callbackService.List();

        }

        [TestMethod]
        public void Create_Callback()
        {
            var callbackService = new CallbackService(BalancedSettings.Secret);
            
            var callbackSent = new Callback
            {
                Url = "http://www.mandoyo.com",
                Method = CallbackMethod.Get,
            };

            var callbackReceived = callbackService.Create(callbackSent);

            Assert.IsNotNull(callbackReceived);
            Assert.IsNotNull(callbackReceived.Id);
            Assert.IsTrue(String.Compare(callbackReceived.Url, callbackSent.Url, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(callbackReceived.Method == callbackSent.Method);
        }

        [TestMethod]
        public void Get_Callback()
        {
            var callbackService = new CallbackService(BalancedSettings.Secret);
            var callback = callbackService.Get(new Callback { Id = BalancedSettings.CallbackTestId });

            Assert.IsNotNull(callback);
            Assert.IsTrue(callback.Id == BalancedSettings.CallbackTestId);
        }

        [TestMethod]
        public void List_Callbacks()
        {
            var callbackService = new CallbackService(BalancedSettings.Secret);
            var items = callbackService.List();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Delete_Callback()
        {
            var callbackService = new CallbackService(BalancedSettings.Secret);

            var callbackSent = new Callback
            {
                Url = "http://www.mandoyo.com",
                Method = CallbackMethod.Get,
            };

            var callbackReceived = callbackService.Create(callbackSent);

            var result = callbackService.Delete(callbackReceived);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Callback()
        {

            var callbackService = new CallbackService(BalancedSettings.Secret);
            var callbackSent = new Callback
            {
                Id = "Mandoyo_Inc_Fake",
            };

            callbackService.Delete(callbackSent);

        }   
    }
}