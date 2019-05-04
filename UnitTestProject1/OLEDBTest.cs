using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace UnitTestProject1
{
    [TestClass]
    public class OLEDBTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = @"E:\1test\各种格式的.xlsx";
            DataTable table =OLEDBLibrary.ExcelHelper.ReadCSVByACEOLEDB(path);

        }
    }
}
