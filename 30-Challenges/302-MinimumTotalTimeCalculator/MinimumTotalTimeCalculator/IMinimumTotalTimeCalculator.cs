namespace MinimumTotalTimeCalculator;

/// <summary>
/// Interface for calculating minimum total processing time
/// </summary>
public interface IMinimumTotalTimeCalculator
{
    /// <summary>
    /// Calculate the minimum total time required to process all customers
    /// </summary>
    /// <param name="cashiers">Number of available cashiers</param>
    /// <param name="customers">Array where each element represents the number of articles per customer</param>
    /// <returns>The minimum total time to process all customers</returns>
    long CalculateMinimalProcessingTime(int cashiers, int[] customers);
}
