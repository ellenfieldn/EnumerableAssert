using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class DoesNotContain
    {
        [TestMethod]
        public void DoesNotContain_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain(null, "test"));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain(collection, "test"));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.DoesNotContain(collection, null as string));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: item", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_NotInCollection()
        {
            var collection = new List<string> { "element", "element2" };
            EnumerableAssert.DoesNotContain(collection, "unexpected element");
        }

        [TestMethod]
        public void DoesNotContain_ElementInCollection()
        {
            var collection = new List<string> { "element", "unexpected element", "element2" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain(collection, "unexpected element"));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection contains the unexpected element.", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_Predicate_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain<string>(null, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_Predicate_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_Predicate_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.DoesNotContain(collection, null as Func<string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void DoesNotContain_Predicate_NotInCollection()
        {
            var collection = new List<string> { "element", "element2" };
            EnumerableAssert.DoesNotContain(collection, s => s.IndexOf("e") == 1);
        }

        [TestMethod]
        public void DoesNotContain_Predicate_ElementInCollection()
        {
            var collection = new List<string> { "element", "unexpected element", "element2" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.DoesNotContain(collection, s => s.IndexOf("u") == 0));
            Assert.AreEqual("EnumerableAssert.DoesNotContain failed. Collection contains an element matching the predicate.", ex.Message);
        }
    }
}
