using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace UnitTestProject1
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            string path = @"e:\1test\time.xlsx";
            string csvpath = @"e:\1test\time.csv";
         
            ExcelDataReaderLibrary.ExcelFileHelper.SaveAsCsv(path, csvpath);
            //
            // TODO:  在此处添加测试逻辑
            //
            //string path = @"E:\1test\各种格式的.xlsx";
            //string csvpath = @"E:\1test\各种格式的1.csv";
            //ConvertExcelToCsv(path, csvpath, 1);
        }

        //public static void SaveAs()
        //{
        //    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

        //    Microsoft.Office.Interop.Excel.Workbook wbWorkbook =  app.Workbooks.Open(@"E:\1test\time.xlsx");

        //    wbWorkbook.SaveAs("E:\\1test\\1111.csv");
        //    //Microsoft.Office.Interop.Excel.Workbook wbWorkbook = app.Workbooks.Add(Type.Missing);
        //    Microsoft.Office.Interop.Excel.Sheets wsSheet = wbWorkbook.Worksheets;
        //    Microsoft.Office.Interop.Excel.Worksheet CurSheet = (Microsoft.Office.Interop.Excel.Worksheet)wsSheet[1];

        //    Microsoft.Office.Interop.Excel.Range thisCell = (Microsoft.Office.Interop.Excel.Range)CurSheet.Cells[1, 1];

        //    thisCell.Value2 = "This is a test.";

        //    wbWorkbook.SaveAs(@"c:\one.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    wbWorkbook.SaveAs(@"c:\two.csv", Microsoft.Office.Interop.Excel.XlFileFormat.xlCSVWindows, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //    wbWorkbook.Close(false, "", true);
        //}

        static void ConvertExcelToCsv(string excelFilePath, string csvOutputFile, int worksheetNumber = 1)
        {
            if (!File.Exists(excelFilePath)) throw new FileNotFoundException(excelFilePath);
            if (File.Exists(csvOutputFile)) throw new ArgumentException("File exists: " + csvOutputFile);

            // connection string
            var cnnStr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;IMEX=1;HDR=NO\"", excelFilePath);
            var cnn = new OleDbConnection(cnnStr);

            // get schema, then data
            var dt = new DataTable();
            try
            {
                cnn.Open();
                var schemaTable = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (schemaTable.Rows.Count < worksheetNumber) throw new ArgumentException("The worksheet number provided cannot be found in the spreadsheet");
                string worksheet = schemaTable.Rows[worksheetNumber - 1]["table_name"].ToString().Replace("'", "");
                string sql = String.Format("select * from [{0}]", worksheet);
                var da = new OleDbDataAdapter(sql, cnn);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                // ???
                throw e;
            }
            finally
            {
                // free resources
                cnn.Close();
            }

            // write out CSV data
            using (var wtr = new StreamWriter(csvOutputFile))
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool firstLine = true;
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (!firstLine) { wtr.Write(","); } else { firstLine = false; }
                        var data = row[col.ColumnName].ToString().Replace("\"", "\"\"");
                        wtr.Write(String.Format("\"{0}\"", data));
                    }
                    wtr.WriteLine();
                }
            }
        }
    }
}
