using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass]
    public class AreNotEquivalent_Predicate
    {
        [TestMethod]
        public void FirstIsNull()
        {
            EnumerableAssert.AreNotEquivalent(null, new List<string> { "test" }, (first, second) => throw new Exception("Should not make it here."));
        }

        [TestMethod]
        public void SecondIsNull()
        {
            EnumerableAssert.AreNotEquivalent(new List<string> { "test" }, null, (first, second) => throw new Exception("Should not make it here."));
        }

        [TestMethod]
        public void FirstAndSecondNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreNotEquivalent<string>(null, null, (first, second) => throw new Exception("Should not make it here.")));
            Assert.AreEqual("EnumerableAssert.AreNotEquivalent failed. Both enumerables are null.", ex.Message);
        }

        [TestMethod]
        public void PredicateNull()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => EnumerableAssert.AreNotEquivalent(new List<string> { "test" }, new List<string> { "test" }, null as Func<string, string, bool>));
            Assert.AreEqual($"Value cannot be null.{Environment.NewLine}Parameter name: predicate", ex.Message);
        }

        [TestMethod]
        public void Equivalent_SameReference()
        {
            var enumerable = new List<string> { "test" };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreNotEquivalent(enumerable, enumerable, (first, second) => throw new Exception("Should not make it here.")));
            Assert.AreEqual("EnumerableAssert.AreNotEquivalent failed. Enumerables refer to the same object.", ex.Message);
        }

        [TestMethod]
        public void Equivalent_BothEmpty()
        {
            var expected = new List<string>();
            var actual = new List<string>();
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreNotEquivalent(expected, actual, (first, second) => throw new Exception("Should not make it here.")));
            Assert.AreEqual("EnumerableAssert.AreNotEquivalent failed. Both enumerables are empty.", ex.Message);
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
                expected[0],
                expected[1],
            };
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreNotEquivalent(expected, actual, (e, a) => e.Id == a.Id));
            Assert.AreEqual("EnumerableAssert.AreNotEquivalent failed. Enumerables are equivalent.", ex.Message);
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
                expected[1],
                expected[0]
            };

            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreNotEquivalent(expected, actual, (e, a) => e.Id == a.Id));
            Assert.AreEqual("EnumerableAssert.AreNotEquivalent failed. Enumerables are equivalent.", ex.Message);
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
            EnumerableAssert.AreNotEquivalent(expected, actual, (e, a) => e.Id == a.Id);
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
            EnumerableAssert.AreNotEquivalent(expected, actual, (e, a) => e.Id == a.Id);
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
            EnumerableAssert.AreNotEquivalent(expected, actual, (e, a) => e.Id == a.Id);
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