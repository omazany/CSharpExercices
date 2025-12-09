namespace LeastRecentlyUsedCache;

// Finish the implementation of the LRU Cache class below.
public class Solution : ILRUCache<int, int>
{
    // CacheItem class to hold key-value pairs
    // Class is reference type so instead of the struct, we can change the Value property directly
    // The common KeyValuePair struct can not be used as it read only and we can't change the Value property
    private class CacheItem
    {
        public int Value;
        public int Key;
        public CacheItem(int key, int value)
        {
            Key = key; 
            Value = value;
        }
    }

    // Dictionary to hold the cache values for O(1) access, this maps keys to linked list nodes which hold the key-value pairs
    // This collection is only used for fast access to the linked list nodes that hold the actual values
    Dictionary<int, LinkedListNode<CacheItem>> cacheValues;

    // Linked list to maintain the order of usage, with the most recently used items at the front, and least recently used at the back
    // it holds key-value pairs so both key and the value are accessible when evicting items, this is where the value is really stored
    LinkedList<CacheItem> leastUsedQueue;

    // Maximum capacity of the cache
    int capacity;

    public Solution(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than 0");
        }

        this.capacity = capacity;
        cacheValues = new Dictionary<int, LinkedListNode<CacheItem>>(capacity);
        leastUsedQueue = new LinkedList<CacheItem>();        
    }

    // Move the accessed node to the front of the least used queue, thanks to LinkedListNode this is fast O(1)
    private void MoveNodeToFront(LinkedListNode<CacheItem> node)
    {
        leastUsedQueue.Remove(node);
        leastUsedQueue.AddFirst(node);        
    }

    private void InsertNewNodeAtFront(LinkedListNode<CacheItem> node)
    {
        // If the cache is at capacity, remove the least recently used item, thanks to the LinkedList this is fast O(1)
        if (leastUsedQueue.Count >= capacity)
        {
            var lruNode = leastUsedQueue.Last;

            if (lruNode != null)
            {
                leastUsedQueue.RemoveLast();

                // don't forget to remove the LinkedListNode also from the Dictionary
                CacheItem item = lruNode.Value;
                cacheValues.Remove(item.Key);
            }
        }

        leastUsedQueue.AddFirst(node);
        // Add the new node to the dictionary for fast access
        cacheValues[node.Value.Key] = node;
    }

    public int Get(int key)
    {
        if (cacheValues.TryGetValue(key, out var linkedListNode))
        {
            // If the node was retrieved, ensure it is marked as most recently used
            MoveNodeToFront(linkedListNode);

            return linkedListNode.Value.Value;
        }
        return -1;
    }

    public void Put(int key, int value)
    {
        LinkedListNode<CacheItem> node;

        // Using dictionary we can quickly determine if the key already exists and get the LinkedListNode with complexity O(1)
        if (cacheValues.TryGetValue(key, out node))
        {            
            CacheItem item = node.Value; // Update the value of the Cache Item
            item.Value = value;            

            MoveNodeToFront(node); // ensure this node is now in front of the least used queue
        }
        else
        {
            // If the key does not exist in the Dictionary, create a new node
            node = new LinkedListNode<CacheItem>(new CacheItem(key, value));
            InsertNewNodeAtFront(node);
        }
    }
}