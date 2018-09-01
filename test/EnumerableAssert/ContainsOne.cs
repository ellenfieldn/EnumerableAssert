using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class ContainsOne
    {
        [TestMethod]
        public void ContainsOne_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(null, "test"));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, "test"));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.ContainsOne(collection, null as string));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: item", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_NoElementMatches_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, "expected"));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection does not contain the expected element.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_OneElementMatches_Size1()
        {
            var collection = new List<string> { "expected" };
            EnumerableAssert.ContainsOne(collection, "expected");
        }

        [TestMethod]
        public void ContainsOne_OneElementMatches_MultipleElementsInList()
        {
            var collection = new List<string> { "expected", "notexpected", "also not expected" };
            EnumerableAssert.ContainsOne(collection, "expected");
        }

        [TestMethod]
        public void ContainsOne_MultipleElementsMatch_ThrowsException()
        {
            var collection = new List<string> { "expected", "notexpected", "expected" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, "expected"));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection contains the expected element more than once.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_Predicate_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne<string>(null, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_Predicate_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_Predicate_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.ContainsOne(collection, null as Func<string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_Predicate_NoElementMatches_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, s => s.IndexOf("e") == 1));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection does not contain an element that matches the predicate.", ex.Message);
        }

        [TestMethod]
        public void ContainsOne_Predicate_OneElementMatches_Size1()
        {
            var collection = new List<string> { "expected" };
            EnumerableAssert.ContainsOne(collection, s => s.IndexOf("e") == 0);
        }

        [TestMethod]
        public void ContainsOne_Predicate_OneElementMatches_MultipleElementsInList()
        {
            var collection = new List<string> { "expected", "notexpected", "also not expected" };
            EnumerableAssert.ContainsOne(collection, s => s.IndexOf("e") == 0);
        }

        [TestMethod]
        public void ContainsOne_Predicate_MultipleElementsMatch_ThrowsException()
        {
            var collection = new List<string> { "expected", "notexpected", "expected as well" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.ContainsOne(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.ContainsOne failed. Collection contains more than one element that matches the predicate.", ex.Message);
        }
    }
}
