using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinimumTotalTimeCalculator;

namespace MinimumTotalTimeCalculator.Tests
{
    [TestClass()]
    public class SolutionTests
    {
        [TestMethod()]
        public void SolutionInstantiationTest()
        {
            var solution = new Solution();
            Assert.IsNotNull(solution);
        }

        [TestMethod()]
        public void Example1_TwoCashiers_FiveCustomers()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 3, 2, 1, 4, 5 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(8, result);
            // Explanation:
            // Time 0: Cashier 1 starts Customer 1 (3 articles), Cashier 2 starts Customer 2 (2 articles)
            // Time 2: Cashier 2 finishes, starts Customer 3 (1 article)
            // Time 3: Cashier 1 finishes, starts Customer 4 (4 articles)
            //         Cashier 2 finishes, starts Customer 5 (5 articles)
            // Time 7: Cashier 1 finishes
            // Time 8: Cashier 2 finishes
        }

        [TestMethod()]
        public void Example2_OneCashier_ThreeCustomers()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 1;
            int[] customers = new int[] { 5, 3, 4 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(12, result);
            // One cashier processes all: 5 + 3 + 4 = 12
        }

        [TestMethod()]
        public void Example3_MoreCashiersThanCustomers()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 5;
            int[] customers = new int[] { 2, 3, 1 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(3, result);
            // All customers processed in parallel, max time is 3
        }

        [TestMethod()]
        public void SingleCustomer_SingleCashier()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 1;
            int[] customers = new int[] { 10 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod()]
        public void EqualArticles_TwoCashiers()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 5, 5, 5, 5 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(10, result);
            // Each cashier processes 2 customers: 5 + 5 = 10
        }

        [TestMethod()]
        public void ThreeCashiers_VariedLoad()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 3;
            int[] customers = new int[] { 1, 2, 3, 4, 5, 6 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(9, result);
            // Distribution: start will be [1,2,3], then [4,5,6] assigned to earliest free cashiers
            // The resulting loads will be [1+4=5, 2+5=7, 3+6=9], max is 9
        }

        [TestMethod()]
        public void AllCustomersOneArticle()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 1, 1, 1, 1, 1 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(3, result);
            // Cashier 1: 1+1+1=3, Cashier 2: 1+1=2
        }

        [TestMethod()]
        public void VeryLargeArticleCount()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 1000000, 1, 1 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(1000000, result);
            // Cashier 1: 1000000, Cashier 2: 1+1=2
        }

        [TestMethod()]
        public void TenCashiers_TenCustomers_EqualLoad()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 10;
            int[] customers = new int[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(5, result);
            // Each cashier processes one customer
        }

        [TestMethod()]
        public void EdgeCase_DescendingOrder()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(28, result);
            // Optimal: Cashier 1: 10+8+6+4+2=30 or Cashier 1: 10+7+5+3+1=26, Cashier 2: 9+8+6+4+1=28
        }

        [TestMethod()]
        public void EdgeCase_AscendingOrder()
        {
            // Arrange
            var solution = new Solution();
            int cashiers = 2;
            int[] customers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Act
            var result = solution.CalculateMinimalProcessingTime(cashiers, customers);

            // Assert
            Assert.AreEqual(30, result);
            
            // The occupacy will be:
            // [1,2] -> [3,4] -> [5,6] -> [7,8] -> [9,10]
            // The total max time wil be 10+8+6+4+2=30
        }
    }
}
