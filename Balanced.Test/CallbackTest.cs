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
    public class CallbackTest
    {

        public CallbackTest()
        {
            BalancedSettings.Init(BalancedTestKeys.BalancedCfg);
        }

        [TestMethod]
        public void Connect_Success()
        {
            var callbackService = new CallbackService();
            var items = callbackService.List();

            Assert.IsNotNull(items);

        }

        [TestMethod]
        public void Create_Callback()
        {
            var callbackService = new CallbackService();
            
            var callbackSent = new Callback
            {
                Url = "http://www.mandoyo.com",
                Method = CallbackMethod.Get,
            };

            var callbackReceived = callbackService.Create(callbackSent);

            Assert.IsNotNull(callbackReceived);
            Assert.IsNotNull(callbackReceived.Callbacks);
            Assert.IsTrue(callbackReceived.Callbacks.Count > 0);
            Assert.IsNotNull(callbackReceived.Callbacks[0].Id);
            Assert.IsTrue(String.Compare(callbackReceived.Callbacks[0].Url, callbackSent.Url, StringComparison.InvariantCultureIgnoreCase) == 0);
            Assert.IsTrue(callbackReceived.Callbacks[0].Method == callbackSent.Method);
        }

        [TestMethod]
        public void Get_Callback()
        {
            var callbackService = new CallbackService();
            var callback = callbackService.Get(new Callback { Id = BalancedTestKeys.CallbackTestId });

            Assert.IsNotNull(callback);
            Assert.IsNotNull(callback.Callbacks);
            Assert.IsTrue(callback.Callbacks.Count > 0);
            Assert.IsTrue(callback.Callbacks[0].Id == BalancedTestKeys.CallbackTestId);
        }

        [TestMethod]
        public void List_Callbacks()
        {
            var callbackService = new CallbackService();
            var items = callbackService.List();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void Delete_Callback()
        {
            var callbackService = new CallbackService();

            var callbackSent = new Callback
            {
                Url = "http://www.mandoyo.com",
                Method = CallbackMethod.Get,
            };

            var callbackReceived = callbackService.Create(callbackSent);

            var result = callbackService.Delete(callbackReceived.Callbacks[0]);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Delete_Fake_Callback()
        {

            var callbackService = new CallbackService();
            var callbackSent = new Callback
            {
                Id = "Mandoyo_Inc_Fake",
            };

            callbackService.Delete(callbackSent);

        }   
    }
}