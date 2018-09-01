using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class IsNullOrEmpty
    {
        [TestMethod]
        public void IsNullOrEmpty_Null()
        {
            EnumerableAssert.IsNullOrEmpty(null);
        }

        [TestMethod]
        public void IsNullOrEmpty_Empty()
        {
            EnumerableAssert.IsNullOrEmpty(new List<string>());
        }

        [TestMethod]
        public void IsNullOrEmpty_NotEmpty_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsNullOrEmpty(collection));
            Assert.AreEqual("EnumerableAssert.IsNullOrEmpty failed. Collection is not null or empty.", ex.Message);
        }
    }
}
