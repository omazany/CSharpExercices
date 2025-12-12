@ -1,22 +0,0 @@
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
        throw new NotImplementedException();
    }
}