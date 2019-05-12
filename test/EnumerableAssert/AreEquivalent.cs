using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert;
using System.Collections.Generic;

namespace EnumerableAssertTests
{
    [TestClass, TestCategory("Unit Tests")]
    public class AreEquivalent
    {
        [TestMethod]
        public void FirstIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(null, new List<string> { "test" }));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Expected enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void SecondIsNull()
        {
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(new List<string> { "test" }, null));
            Assert.AreEqual("EnumerableAssert.AreEquivalent failed. Actual enumerable is null.", ex.Message);
        }

        [TestMethod]
        public void FirstAndSecondNull()
        {
            EnumerableAssert.AreEquivalent<string>(null, null);
        }

        [TestMethod]
        public void Equivalent_SameReference()
        {
            var enumerable = new List<string> { "test" };
            EnumerableAssert.AreEquivalent(enumerable, enumerable);
        }

        [TestMethod]
        public void Equivalent_BothEmpty()
        {
            var expected = new List<string>();
            var actual = new List<string>();
            EnumerableAssert.AreEquivalent(expected, actual);
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

            EnumerableAssert.AreEquivalent(expected, actual);
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

            EnumerableAssert.AreEquivalent(expected, actual);
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
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual));
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
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual));
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
            var ex = Assert.ThrowsException<AssertFailedException>(() => EnumerableAssert.AreEquivalent(expected, actual));
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
