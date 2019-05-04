using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class TupleTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testTuple6 = new Tuple<int, int, int, int, int, int>(1, 2, 3, 4, 5, 6);
            Console.WriteLine($"Item 1: {testTuple6.Item1}, Item 6: {testTuple6.Item6}");

            var testTuple10 = new Tuple<int, int, int, int, int, int, int, Tuple<int, int, int>>(1, 2, 3, 4, 5, 6, 7, new Tuple<int, int, int>(8, 9, 10));
            Console.WriteLine($"Item 1: {testTuple10.Item1}, Item 10: {testTuple10.Rest.Item3}");
           
        }
    }
}
