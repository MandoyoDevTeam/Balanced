using Balanced.Entities;
using Balanced.Exceptions;
using Balanced.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class MarketPlaceTest
    {

        [TestMethod]
        public void Connect_Success()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            var items = marketplaceService.List();

            Assert.IsNotNull(items);
            
        }

        [TestMethod]
        [ExpectedException(typeof(BalancedException))]
        public void Connect_FakeSecret()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.FakeSecret);
            //should throws an exception
            marketplaceService.List();
        }

        [TestMethod]
        public void Get_Success()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            var marketplace = marketplaceService.Get(new Marketplace { Id = BalancedSettings.MarketplaceTestId });

            Assert.IsNotNull(marketplace);
            Assert.IsTrue(marketplace.Id == BalancedSettings.MarketplaceTestId);
        }

        [TestMethod]
        public void List_Success()
        {
            var marketplaceService = new MarketplaceService(BalancedSettings.Secret);
            var items = marketplaceService.List();

            Assert.IsNotNull(items);
            Assert.IsNotNull(items.Items);
            Assert.IsTrue(items.Items.Count > 0);
        }
    }
}