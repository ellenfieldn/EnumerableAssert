using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class AreEquivalent
    {
        [TestMethod]
        public void SourceIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(null, new List<string> { "test" }, (s, t) => s.IndexOf("t") == t.IndexOf("t")));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Source enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void TargetIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(new List<string> { "test" }, null, (s,t) => s.IndexOf("t") == t.IndexOf("t")));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Target enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void PredicateNull()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.AreEquivalent(new List<string> { "test" }, new List<string> { "test" }, null as Func<string, string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void SourceIsEquivalent_SameOrder()
        {
            var source = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1"),
            };
            var target = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "not1"),
            };

            EnumerableAssert.AreEquivalent(source, target, (s, t) => s.Id == t.Id);
        }

        [TestMethod]
        public void SourceIsEquivalent_DifferentOrder()
        {
            var source = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var target = new List<TestType>
            {
                new TestType(1, "not1"),
                new TestType(0, "0")
            };

            EnumerableAssert.AreEquivalent(source, target, (s, t) => s.Id == t.Id);
        }

        [TestMethod]
        public void SourceIsNotEquivalent_OneElementDifference()
        {
            var source = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var target = new List<TestType>
            {
                new TestType(2, "not1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(source, target, (s, t) => s.Id == t.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Source and target enumerables do not match.", ex.Message);
        }

        [TestMethod]
        public void SourceIsNotEquivalent_SourceHasExtraElement()
        {
            var source = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1"),
                new TestType(2, "not1"),
            };
            var target = new List<TestType>
            {
                new TestType(1, "1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(source, target, (s, t) => s.Id == t.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Source and target enumerables do not match.", ex.Message);
        }

        [TestMethod]
        public void SourceIsNotEquivalent_TargetHasExtraElement()
        {
            var source = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var target = new List<TestType>
            {
                new TestType(2, "not1"),
                new TestType(1, "1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(source, target, (s, t) => s.Id == t.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Source and target enumerables do not match.", ex.Message);
        }

        public class TestType
        {
            public int Id;
            public string Value;

            public TestType(int id, string value)
            {
                Id = id;
                Value = value;
            }
        }
    }
}
