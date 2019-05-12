using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class Extensibility
    {
        [TestMethod]
        public void ThatTest()
        {
            var that = EnumerableAssert.That;
            Assert.IsNotNull(that);
        }

        [TestMethod]
        public void ExtensionMethodTest()
        {
            var result = EnumerableAssert.That.ReturnString();
            Assert.AreEqual("success!", result);
        }
    }

    public static class TestExtensions
    {
        public static string ReturnString(this EnumerableAssert enumerableAssert) => "success!";
    }
}
