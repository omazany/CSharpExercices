# Least Recently Used Cache

## Instructions

Implement a class called `Solution` that implements the `ILRUCache` interface.

The class must have a parameterized constructor with an `int` parameter that initializes the cache capacity. The capacity is fixed and cannot be exceeded.

The methods of the interface `Get` and `Put` should have average O(1) time complexity.

The cache will return the stored value based on the key. Adding a new value to the cache will add a new entry if there is available capacity. Otherwise, it will remove the least recently used value from the cache and add the new one.

The `Get` method should return `-1` if the key is not found.

The solution must pass all the tests.

## Example

Assume the cache is initialized with a capacity of 2:

```csharp
Solution cache = new Solution(2);
cache.Put(1, 1); // cache is {1=1}
cache.Put(2, 2); // cache is {1=1, 2=2}
cache.Get(1);    // returns 1, cache is {2=2, 1=1} (1 is now most recently used)
cache.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
cache.Get(2);    // returns -1 (not found)
cache.Put(4, 4); // LRU key was 1, evicts key 1, cache is {3=3, 4=4}
cache.Get(1);    // returns -1 (not found)
cache.Get(3);    // returns 3
cache.Get(4);    // returns 4
```

## Constraints

- 1 <= capacity <= 3000
- 0 <= key <= 10^4
- 0 <= value <= 10^5

