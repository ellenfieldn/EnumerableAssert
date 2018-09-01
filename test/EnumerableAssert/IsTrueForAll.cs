using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class IsTrueForAll
    {
        [TestMethod]
        public void IsTrueForAll_Predicate_CollectionNull_ThrowsException()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsTrueForAll<string>(null, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.IsTrueForAll failed. Collection is null.", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_CollectionEmpty_ThrowsException()
        {
            var collection = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.IsTrueForAll failed. Collection is empty.", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_ExpectedItemNull_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.IsTrueForAll(collection, null as Func<string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_NoElementMatches_ThrowsException()
        {
            var collection = new List<string> { "element" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 1));
            Assert.AreEqual("EnumerableAssert.IsTrueForAll failed. No elements match the predicate.", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_OneElementMatches_Size1()
        {
            var collection = new List<string> { "expected" };
            EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 0);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_OneElementMatches_MultipleElementsInList()
        {
            var collection = new List<string> { "expected", "notexpected", "also not expected" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.IsTrueForAll failed. Not all elements match the predicate.", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_NotAllElementsMatch()
        {
            var collection = new List<string> { "expected", "notexpected", "expected as well" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 0));
            Assert.AreEqual("EnumerableAssert.IsTrueForAll failed. Not all elements match the predicate.", ex.Message);
        }

        [TestMethod]
        public void IsTrueForAll_Predicate_AllElementsMatch()
        {
            var collection = new List<string> { "expected", "extra expected", "expected as well" };
            EnumerableAssert.IsTrueForAll(collection, s => s.IndexOf("e") == 0);
        }
    }
}
