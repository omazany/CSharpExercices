using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeastRecentlyUsedCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeastRecentlyUsedCache.Tests
{
    [TestClass()]
    public class SolutionTests
    {
        [TestMethod()]
        public void SolutionInstantiationTest()
        {
            var solution = new Solution(100);

            Assert.IsNotNull(solution);
        }

        [TestMethod()]
        public void GetTest()
        {
            var solution = new Solution(2);

            var result = solution.Get(1);

            Assert.AreEqual(-1, result);
        }

        [TestMethod()]
        public void PutTest()
        {
            var solution = new Solution(2);

            solution.Put(1, 1);

            var result = solution.Get(1);

            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void Example1Test()
        {
            var solution = new Solution(2);
            solution.Put(1, 1); // cache is {1=1}
            solution.Put(2, 2); // cache is {1=1, 2=2}
            var result1 = solution.Get(1);    // return 1
            Assert.AreEqual(1, result1);
            solution.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}
            var result2 = solution.Get(2);    // returns -1 (not found)
            Assert.AreEqual(-1, result2);
            solution.Put(4, 4); // LRU key was 1, evicts key 1, cache is {4=4, 3=3}
            var result3 = solution.Get(1);    // return -1 (not found)
            Assert.AreEqual(-1, result3);
            var result4 = solution.Get(3);    // return 3
            Assert.AreEqual(3, result4);
            var result5 = solution.Get(4);    // return 4
            Assert.AreEqual(4, result5);
        }

        [TestMethod()]
        public void Example2Test()
        {
            var solution = new Solution(1);
            solution.Put(2, 1); // cache is {2=1}
            var result1 = solution.Get(2);    // return 1
            Assert.AreEqual(1, result1);
            solution.Put(3, 2); // LRU key was 2, evicts key 2, cache is {3=2}
            var result2 = solution.Get(2);    // returns -1 (not found)
            Assert.AreEqual(-1, result2);
            var result3 = solution.Get(3);    // return 2
            Assert.AreEqual(2, result3);
        }

        [TestMethod()]
        public void Example3Test()
        {
            var solution = new Solution(2);
            var result1 = solution.Get(2);    // return -1
            Assert.AreEqual(-1, result1);
            solution.Put(2, 6); // cache is {2=6}
            var result2 = solution.Get(1);    // return -1
            Assert.AreEqual(-1, result2);
            solution.Put(1, 5); // cache is {2=6, 1=5}
            solution.Put(1, 2); // cache is {2=6, 1=2}
            var result3 = solution.Get(1);    // return 2
            Assert.AreEqual(2, result3);
            var result4 = solution.Get(2);    // return 6
            Assert.AreEqual(6, result4);
        }
    }
}