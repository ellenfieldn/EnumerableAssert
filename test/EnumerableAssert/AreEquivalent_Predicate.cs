using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class AreEquivalent_Predicate
    {
        [TestMethod]
        public void FirstIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(null, new List<string> { "test" }, (s, t) => s.IndexOf("t") == t.IndexOf("t")));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Expected enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void SecondIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(new List<string> { "test" }, null, (s, t) => s.IndexOf("t") == t.IndexOf("t")));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Actual enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void FirstAndSecondNull()
        {
            EnumerableAssert.AreEquivalent<string>(null, null, (e, a) => e.IndexOf("t") == a.IndexOf("t"));
        }

        [TestMethod]
        public void PredicateNull()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.AreEquivalent(new List<string> { "test" }, new List<string> { "test" }, null as Func<string, string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void Equivalent_SameReference()
        {
            var enumerable = new List<string> { "test" };
            EnumerableAssert.AreEquivalent(enumerable, enumerable, (e, a) => throw new Exception("Shouldn't get this"));
        }

        [TestMethod]
        public void Equivalent_BothEmpty()
        {
            var expected = new List<string>();
            var actual = new List<string>();
            EnumerableAssert.AreEquivalent(expected, actual, (e, a) => throw new Exception("Shouldn't get this"));
        }

        [TestMethod]
        public void Equivalent_SameOrder()
        {
            var expected = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1"),
            };
            var actual = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "not1"),
            };

            EnumerableAssert.AreEquivalent(expected, actual, (e, a) => e.Id == a.Id);
        }

        [TestMethod]
        public void Equivalent_DifferentOrder()
        {
            var expected = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var actual = new List<TestType>
            {
                new TestType(1, "not1"),
                new TestType(0, "0")
            };

            EnumerableAssert.AreEquivalent(expected, actual, (e, a) => e.Id == a.Id);
        }

        [TestMethod]
        public void NotEquivalent_OneElementDifference()
        {
            var expected = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var actual = new List<TestType>
            {
                new TestType(2, "not1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual, (e, a) => e.Id == a.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.", ex.Message);
        }

        [TestMethod]
        public void NotEquivalent_ExpectedHasExtraElement()
        {
            var expected = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1"),
                new TestType(2, "not1"),
            };
            var actual = new List<TestType>
            {
                new TestType(1, "1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual, (e, a) => e.Id == a.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.", ex.Message);
        }

        [TestMethod]
        public void NotEquivalent_ActualHasExtraElement()
        {
            var expected = new List<TestType>
            {
                new TestType(0, "0"),
                new TestType(1, "1")
            };
            var actual = new List<TestType>
            {
                new TestType(2, "not1"),
                new TestType(1, "1"),
                new TestType(0, "0")
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual, (e, a) => e.Id == a.Id));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Expected and actual enumerables do not match.", ex.Message);
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
