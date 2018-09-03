# EnumerableAssert
Provides assertions for `IEnumerable<T>` that follow the same patterns as the assertion classes provided in MSTest.TestFramework

Example:
```csharp
var collection = new List<string> { "element", "unexpected element", "element2" };
EnumerableAssert.DoesNotContain(collection, s => s.IndexOf("u") == 0); //Throws an exception
```

## API
- EnumerableAssert.IsNullOrEmpty(IEnumerable)
- EnumerableAssert.Contains<T>(IEnumerable<T>, T) - Contains the element at least once
- EnumerableAssert.Contains<T>(IEnumerable<T>, Func<T, bool>) - Contains at least one element matching the predicate
- EnumerableAssert.ContainsOne<T>(IEnumerable<T>, T) - Enumerable contains the element exactly once
- EnumerableAssert.ContainsOne<T>(IEnumerable<T>, Func<T, bool>) - Enumerable contains exactly one element that matches the predicate
- EnumerableAssert.IsTrueForAll<T>(IEnumerable<T>, Func<T, bool>) - Predicate is true for all elements in the collection
- EnumerableAssert.DoesNotContain<T>(IEnumerable<T>, T) - Enumerable does not contain the element
- EnumerableAssert.DoesNotContain<T>(IEnumerable<T>, Func<T, bool>) - Enumerable does not contain an element matching the predicate

## Plan
In no particular order:

- Add assertions for non-generic collections such as `IEnumerable`.
- Each method will contain overloads like the following
  - `EnumerableAssert.IsNullOrEmpty<T>(IEnumerable<T>)`
  - `EnumerableAssert.IsNullOrEmpty<T>(IEnumerable<T>, String)`
  - `EnumerableAssert.IsNullOrEmpty<T>(IEnumerable<T>, String, Object[])`
 - Allow for extensibility: `EnumerableAssert.That.MyAssertionMethod`

