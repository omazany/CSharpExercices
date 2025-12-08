namespace LeastRecentlyUsedCache;

interface ILRUCache<TKey, TValue>
{
    TValue Get(TKey key);
    void Put(TKey key, TValue value);
}
