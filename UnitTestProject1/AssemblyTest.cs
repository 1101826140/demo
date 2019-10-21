using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorDemo.OperateUtil;
using CalculatorDemo.Factory;
using System.Reflection;

namespace UnitTestProject1
{
    [TestClass]
    public class AssemblyTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //OperateContext context = new OperateContext(123, 1, "-");

            //int result = context.Calculation();

            Assembly assembly = Assembly.Load("OLEDBLibrary.ExcelHelper");

            Type type = assembly.GetType();
            string abc = "sd";
        }



    }
}
