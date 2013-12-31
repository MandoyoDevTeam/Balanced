using Balanced.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Balanced.Test
{
    [TestClass]
    public class BalancedSerializerTest
    {

        private const string StandardString = "mandoyo";
        private const string SerializedString = "\"mandoyo\"";
                

        [TestMethod]
        public void Serialize_Success()
        {
            var result = BalancedJsonSerializer.Serialize(StandardString);
            Assert.AreEqual(result, SerializedString);
        }

        [TestMethod]
        public void Deserialize_Success()
        {
            var result = BalancedJsonSerializer.DeSerialize<string>(SerializedString);
            Assert.AreEqual(result, StandardString);
        }
    }
}
