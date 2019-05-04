using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using System.Text;


//Encoding.GetEncoding("unicode").GetBytes(new char[] { txt_char.Text[0] })[1] == 0
namespace UnitTestProject1
{
    [TestClass]
    public class NPOITest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string path = @"E:\1test\hszj_filter_result.xls";
            string newxlsxpath = @"E:\1test\生成后的excel.xls";
            string newcsvpath = @"E:\1test\生成后的csv.csv";
            //读取excel
            DataTable data = ExcelHelper.Read(path, "Sheet1");

            //  ExcelHelper.Write(data, newxlsxpath);

            //导入csv，打开是乱码的
            //ExcelHelper.Write(data, newcsvpath);

            //npoi根据逗号分隔，写入时格式乱套
            // CSVHelper.WriteCSV(newcsvpath, data);
            CSVHelper2.SaveCSV(data, newcsvpath);


        }
    }
    public class CSVHelper2
    {
        /// <summary>
        /// 写入CSV文件
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="fileName">文件全名</param>
        /// <returns>是否写入成功</returns>
        public static bool SaveCSV(DataTable dt, string fullFileName = "")
        {
            if (fullFileName == "")
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "CSV文件(*.csv)|*.csv";
                sfd.DefaultExt = "csv";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fullFileName = sfd.FileName;
                }
                else
                {
                    return false;
                }
            }
            try
            {
                FileStream fs = new FileStream(fullFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                string data = "";

                //写出列名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);

                //写出各行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        data += dt.Rows[i][j].ToString();
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }

                sw.Close();
                fs.Close();

                return true;
            }
            catch (Exception e)
            {
               
                return false;
            }

        }

        /// <summary>
        /// 打开CSV 文件
        /// </summary>
        /// <param name="fileName">文件全名</param>
        /// <returns>DataTable</returns>
        public static DataTable OpenCSV(string fileName = "")
        {
            if (fileName == "")
            {
               
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.Filter = "CSV文件(*.csv)|*.csv";
                ofd.DefaultExt = "csv";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    return OpenCSV(fileName, 0, 0, 0, 0, true);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                try
                {
                    return OpenCSV(fileName, 0, 0, 0, 0, true);
                }
                catch (Exception e)
                {
                
                    return null;
                }

            }
        }

        /// <summary>
        /// 打开CSV 文件
        /// </summary>
        /// <param name="fileName">文件全名</param>
        /// <param name="firstRow">开始行</param>
        /// <param name="firstColumn">开始列</param>
        /// <param name="getRows">获取多少行</param>
        /// <param name="getColumns">获取多少列</param>
        /// <param name="haveTitleRow">是有标题行</param>
        /// <returns>DataTable</returns>
        public static DataTable OpenCSV(string fullFileName, Int16 firstRow = 0, Int16 firstColumn = 0, Int16 getRows = 0, Int16 getColumns = 0, bool haveTitleRow = true)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //是否已建立了表的字段
            bool bCreateTableColumns = false;
            //第几行
            int iRow = 1;

            //去除无用行
            if (firstRow > 0)
            {
                for (int i = 1; i < firstRow; i++)
                {
                    sr.ReadLine();
                }
            }

            // { ",", ".", "!", "?", ";", ":", " " };
            string[] separators = { "," };
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                strLine = strLine.Trim();
                aryLine = strLine.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

                if (bCreateTableColumns == false)
                {
                    bCreateTableColumns = true;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = firstColumn; i < (getColumns == 0 ? columnCount : firstColumn + getColumns); i++)
                    {
                        DataColumn dc
                            = new DataColumn(haveTitleRow == true ? aryLine[i] : "COL" + i.ToString());
                        dt.Columns.Add(dc);
                    }

                    bCreateTableColumns = true;

                    if (haveTitleRow == true)
                    {
                        continue;
                    }
                }


                DataRow dr = dt.NewRow();
                //for (int j = firstColumn; j < (getColumns == 0 ? columnCount : firstColumn + getColumns); j++)
                //{
                //    dr[j - firstColumn] = aryLine[j];
                //}
                for (int j = firstColumn; j < aryLine.Length; j++)
                {
                    dr[j - firstColumn] = aryLine[j];
                }
                dt.Rows.Add(dr);

                iRow = iRow + 1;
                if (getRows > 0)
                {
                    if (iRow > getRows)
                    {
                        break;
                    }
                }

            }

            sr.Close();
            fs.Close();
            return dt;
        }
    } 
    public class CSVHelper
    {
        /// <summary>
        /// 写入CSV
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dt">要写入的datatable</param>
        public static void WriteCSV(string fileName, DataTable dt)
        {
            FileStream fs;
            StreamWriter sw;
            string data = null;

            //判断文件是否存在,存在就不再次写入列名
            if (!File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);

                //写出列名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";//中间用，隔开
                    }
                }
                sw.WriteLine(data);
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.UTF8);
            }

            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = null;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";//中间用，隔开
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
        }



        /// <summary>
        /// 读取CSV文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public static DataTable ReadCSV(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            //记录每次读取的一行记录
            string strLine = null;
            //记录每行记录中的各字段内容
            string[] arrayLine = null;
            //分隔符
            string[] separators = { "," };
            //判断，若是第一次，建立表头
            bool isFirst = true;

            //逐行读取CSV文件
            while ((strLine = sr.ReadLine()) != null)
            {
                strLine = strLine.Trim();//去除头尾空格
                arrayLine = strLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);//分隔字符串，返回数组
                int dtColumns = arrayLine.Length;//列的个数

                if (isFirst)  //建立表头
                {
                    for (int i = 0; i < dtColumns; i++)
                    {
                        dt.Columns.Add(arrayLine[i]);//每一列名称
                    }
                }
                else   //表内容
                {
                    DataRow dataRow = dt.NewRow();//新建一行
                    for (int j = 0; j < dtColumns; j++)
                    {
                        dataRow[j] = arrayLine[j];
                    }
                    dt.Rows.Add(dataRow);//添加一行
                }
            }
            sr.Close();
            fs.Close();

            return dt;
        }
    }

    public class ExcelHelper
    {

        /// <summary>
        /// 导出表格数据到 xls
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        public static void Write(DataTable dt, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
            {
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet(dt.TableName);
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {

                    fs.Seek(0, SeekOrigin.Begin);
                    book.Write(fs);
                }
                book = null;
            }
        }
        /// <summary>
        /// WEBAPI 下载Excel表格文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="dt"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        //public static HttpResponseMessage DownLoadExcel(this HttpRequestMessage request, DataTable dt, String filename)
        //{
        //    var response = request.CreateResponse(HttpStatusCode.OK);
        //    HSSFWorkbook book = new HSSFWorkbook();
        //    ISheet sheet = book.CreateSheet(dt.TableName);
        //    IRow row = sheet.CreateRow(0);
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
        //    }
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        IRow row2 = sheet.CreateRow(i + 1);
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
        //        }
        //    }
        //    // 写入到客户端  
        //    System.IO.MemoryStream stream = new System.IO.MemoryStream(8192);
        //    book.Write(stream);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    response.Content = new StreamContent(stream);
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //    {
        //        FileName = filename
        //    };
        //    book = null;
        //    return response;
        //}



        /// <summary>
        /// 从 Xls文件导入数据到 DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static DataTable Read(string filePath, String SheetName)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheet(SheetName);
            if (sheet == null)
            {
                return new DataTable();
            }

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable(SheetName);

            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                ICell cell = sheet.GetRow(0).Cells[j];
                dt.Columns.Add(cell.ToString());
            }
            rows.MoveNext();
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow Row = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    Row[i] = cell == null ? null : cell.ToString();
                }
                dt.Rows.Add(Row);
            }
            hssfworkbook.Close();
            hssfworkbook = null;
            return dt;
        }

        /// <summary>
        /// 从 Xls文件导入数据到 DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static DataTable Import(string filePath, Int32 SheetIndex)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(SheetIndex);
            String SheetName = hssfworkbook.GetSheetName(SheetIndex);
            if (sheet == null)
            {
                return new DataTable();
            }

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable(SheetName);

            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                ICell cell = sheet.GetRow(0).Cells[j];
                dt.Columns.Add(cell.ToString());
            }
            rows.MoveNext();
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow Row = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    Row[i] = cell == null ? null : cell.ToString();
                }
                dt.Rows.Add(Row);
            }
            hssfworkbook.Close();
            hssfworkbook = null;
            return dt;
        }



    }
}
