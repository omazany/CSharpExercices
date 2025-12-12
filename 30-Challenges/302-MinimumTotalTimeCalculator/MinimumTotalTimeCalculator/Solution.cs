using System.Collections.Generic;

namespace MinimumTotalTimeCalculator;

/// <summary>
/// Implement the minimum total processing time calculator
/// Use a priority queue to efficiently assign customers to cashiers
/// </summary>
public class Solution : IMinimumTotalTimeCalculator
{
    /// <summary>
    /// Calculate the minimum total time required to process all customers
    /// </summary>
    /// <param name="cashiers">Number of available cashiers</param>
    /// <param name="customers">Array where each element represents the number of articles per customer</param>
    /// <returns>The minimum total time to process all customers</returns>
    public long CalculateMinimalProcessingTime(int cashiers, int[] customers)
    {
        // TODO: Implement the solution using a priority queue
        // Hint: Track when each cashier will be free
        // Always assign the next customer to the cashier that will be free earliest

        PriorityQueue<int, int> cashierQueue = new();    

        int customerIndex = 0; // currently processing customer index

        // First distribute initial customers to available cashiers
        for (int i = 0; i < cashiers && customerIndex < customers.Length; i++)
        {
            cashierQueue.Enqueue(customers[customerIndex], customers[customerIndex]);
            customerIndex++;
        }

        long minimumTotalTime = 0;

        // Process remaining customers, we will dequeue the cashier that will be free the earliest
        // assign new customer to it and count the time needed to free that cashier
        while (customerIndex < customers.Length)
        {
            // Get the cashier that will be free the earliest
            cashierQueue.TryDequeue(out int currentTime, out _);

            // update the time needed to free the cashier again after the processing the next customer
            int nextFreeTime = currentTime + customers[customerIndex];

            // Enqueue the updated time for this cashier
            cashierQueue.Enqueue(nextFreeTime, nextFreeTime);

            customerIndex++;
        }

        // Finally deque all cashiers to find the maximum time taken
        while (cashierQueue.Count > 0)
        {
            minimumTotalTime =  cashierQueue.Dequeue();            
        }
        
        return minimumTotalTime;
    }
}
