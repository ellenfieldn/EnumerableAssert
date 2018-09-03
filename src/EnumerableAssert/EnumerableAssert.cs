using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.VisualStudio.TestTools.UnitTesting.Contrib.EnumerableAssert
{
    public sealed class EnumerableAssert
    {
        private static EnumerableAssert that;

        private EnumerableAssert()
        {
        }

        public static EnumerableAssert That
        {
            get
            {
                if(that == null)
                {
                    that = new EnumerableAssert();
                }
                return that;
            }
        }

        public static void IsNullOrEmpty(IEnumerable enumerable)
        {
            if (enumerable != null && enumerable.GetEnumerator().MoveNext())
            {
                throw new AssertFailedException("EnumerableAssert.IsNullOrEmpty failed. Collection is not null or empty.");
            }
        }

        public static void Contains<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is null.");
            }
            if(!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is empty.");
            }
            if(!enumerable.Contains(item))
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection does not contain the expected element.");
            }
        }

        public static void Contains<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection is empty.");
            }
            if (!enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.Contains failed. Collection does not contain an element that matches the predicate.");
            }
        }

        public static void ContainsOne<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is empty.");
            }
            var matches = enumerable.Where(i => i.Equals(item)).Count();
            if(matches == 0)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection does not contain the expected element.");
            }
            if(matches > 1)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection contains the expected element more than once.");
            }
        }

        public static void ContainsOne<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection is empty.");
            }
            var matches = enumerable.Where(predicate).Count();
            if (matches == 0)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection does not contain an element that matches the predicate.");
            }
            if (matches > 1)
            {
                throw new AssertFailedException("EnumerableAssert.ContainsOne failed. Collection contains more than one element that matches the predicate.");
            }
        }

        public static void IsTrueForAll<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Collection is empty.");
            }
            if (!enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. No elements match the predicate.");
            }
            if (!enumerable.All(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.IsTrueForAll failed. Not all elements match the predicate.");
            }
        }

        public static void DoesNotContain<T>(IEnumerable<T> enumerable, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is empty.");
            }
            if (enumerable.Contains(item))
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection contains the unexpected element.");
            }
        }

        public static void DoesNotContain<T>(IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (enumerable == null)
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is null.");
            }
            if (!enumerable.Any())
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection is empty.");
            }
            if(enumerable.Any(predicate))
            {
                throw new AssertFailedException("EnumerableAssert.DoesNotContain failed. Collection contains an element matching the predicate.");
            }
        }
    }
}
