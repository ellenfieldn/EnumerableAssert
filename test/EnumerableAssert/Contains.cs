using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class Contains
    {
        [TestMethod]
        public void Contains_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains(null, "test"));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void Contains_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains(collection, "test"));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void Contains_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.Contains(collection, null as string));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: item", ex.Message);
        }

        [TestMethod]
        public void Contains_NoElementMatches_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains(collection, "expected"));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection does not contain the expected element.", ex.Message);
        }

        [TestMethod]
        public void Contains_OneElementMatches_Size1()
        {
            var collection = new List<string> { "expected" };
            EnumerableAssert.Contains(collection, "expected");
        }

        [TestMethod]
        public void Contains_OneElementMatches_MultipleElementsInList()
        {
            var collection = new List<string> { "expected", "notexpected", "also not expected" };
            EnumerableAssert.Contains(collection, "expected");
        }

        [TestMethod]
        public void Contains_MultipleElementsMatch()
        {
            var collection = new List<string> { "expected", "notexpected", "expected" };
            EnumerableAssert.Contains(collection, "expected");
        }

        [TestMethod]
        public void Contains_Predicate_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains<string>(null, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void Contains_Predicate_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void Contains_Predicate_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.Contains(collection, null as Func<string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void Contains_Predicate_NoElementMatches_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.Contains(collection, s => s.IndexOf("e") == 1));
            Assert.AreEqual("EnumerableAssert.Contains failed. Collection does not contain an element that matches the predicate.", ex.Message);
        }

        [TestMethod]
        public void Contains_Predicate_OneElementMatches_Size1()
        {
            var collection = new List<string> { "expected" };
            EnumerableAssert.Contains(collection, s => s.IndexOf("e") == 0);
        }

        [TestMethod]
        public void Contains_Predicate_OneElementMatches_MultipleElementsInList()
        {
            var collection = new List<string> { "expected", "notexpected", "also not expected" };
            EnumerableAssert.Contains(collection, s => s.IndexOf("e") == 0);
        }

        [TestMethod]
        public void Contains_Predicate_MultipleElementsMatch()
        {
            var collection = new List<string> { "expected", "notexpected", "expected as well" };
            EnumerableAssert.Contains(collection, s => s.IndexOf("e") == 0);
        }
    }
}
