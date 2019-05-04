using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace OLEDBLibrary
{
    public class ExcelHelper
    {
        public const string Excel2003 = ".xls";

        public const string Excel2007 = ".xlsx";

        public enum ConnectionMode
        {
            Read,
            Write
        }

        private static OleDbConnection GetConnection(string file, ConnectionMode mode)
        {
            if (File.Exists(file))
            {
                var extension = Path.GetExtension(file);
                var connectionString = "";
                if (ExcelHelper.Excel2003.Equals(extension, StringComparison.CurrentCultureIgnoreCase))
                {
                    connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"{0}\";Extended Properties=\"Excel 8.0;HDR=yes;{1}\";",
                        file,
                        (mode == ConnectionMode.Read ? "IMEX=1" : "IMEX=2")
                        );
                }
                else if (ExcelHelper.Excel2007.Equals(extension, StringComparison.CurrentCultureIgnoreCase))
                {
                    connectionString = String.Format("Provider=Microsoft.Ace.OLEDB.12.0;Data Source=\"{0}\";Extended Properties=\"Excel 12.0;HDR=yes;{1}\";",
                        file, (mode == ConnectionMode.Read ? "IMEX=1" : ""));
                }
                return new OleDbConnection(connectionString);
            }
            else
            {
                throw new FileNotFoundException();
            }

        }


        /// <summary>
        ///ConnectionMode is read
        /// </summary>
        /// <param name="file"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDatatable(string file, string commandText, params OleDbParameter[] cmdParameters)
        {
            return ExecuteDatatable(file, commandText, ConnectionMode.Read, cmdParameters);
        }


        public static DataTable ExecuteDatatable(string file, string commandText, ConnectionMode mode, params OleDbParameter[] cmdParameters)
        {
            using (OleDbConnection conn = GetConnection(file, mode))
            using (OleDbCommand cmd = new OleDbCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = commandText;
                if (cmdParameters != null && cmdParameters.Length > 0)
                {
                    foreach (OleDbParameter param in cmdParameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }


        /// <summary>
        /// ConnectionMode is write
        /// </summary>
        /// <param name="file"></param>
        /// <param name="commandText"></param>
        /// <param name="cmdParameters"></param>
        public static void ExecuteNonQuery(string file, string commandText, params OleDbParameter[] cmdParameters)
        {
            ExecuteNonQuery(file, commandText, ConnectionMode.Write, cmdParameters);
        }

        public static void ExecuteNonQuery(string file, string commandText, ConnectionMode mode, params OleDbParameter[] cmdParameters)
        {
            using (OleDbConnection conn = GetConnection(file, mode))
            using (OleDbCommand cmd = new OleDbCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = commandText;
                if (cmdParameters != null && cmdParameters.Length > 0)
                {
                    foreach (OleDbParameter param in cmdParameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static DataTable ReadCSVByACEOLEDB(string excelFilename)
        {
            string connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{0}\";Extended Properties=\"Text\"", Directory.GetParent(excelFilename));
            DataSet ds = new DataSet();
            string fileName = string.Empty;
            using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
            {
                connection.Open();
                fileName = Path.GetFileName(excelFilename);

                string strExcel = "select * from " + "[" + fileName + "]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, connectionString);
                adapter.Fill(ds, fileName);
                connection.Close();
                //tableNames.Clear();
            }
            return ds.Tables[fileName];
        }

        /// <summary>
        /// "HDR=Yes;"声名第一行的数据为域名，并非数据。 
        /// 这种方式读取csv文件前8条为int类型后面为string类型 则后面数据读取不了
        /// 还存在乱码问题
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static DataTable Read(string fullPath, string sheetname = "sheet1$")
        {
            FileInfo fileInfo = new FileInfo(fullPath);
            DataTable table = new DataTable();
            string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileInfo.DirectoryName + ";Extended Properties='Text;HDR=YES;FMT=Delimited;IMEX=1;'";
            string cmdstring = String.Format("select * from [{0}]", sheetname);

            using (OleDbConnection conn = new OleDbConnection(connstring))
            {
                conn.Open();

                OleDbDataAdapter adapter = new OleDbDataAdapter(cmdstring, conn);
                adapter.Fill(table);

                conn.Close();
            }

            return table;
        }

        public static string ExportToCSV(DataTable table)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (i == table.Columns.Count - 1)
                {
                    sb.Append(table.Columns[i].Caption);
                }
                else
                {
                    sb.AppendFormat("{0},", table.Columns[i].Caption);
                }
            }
            sb.Append(Environment.NewLine);

            for (int index = 0; index < table.Rows.Count; index++)
            {
                StringBuilder sb2 = new StringBuilder();
                DataRow row = table.Rows[index];

                for (int i = 0; i < table.Columns.Count; i++)
                {

                    string input = row[i].ToString();
                    string format = "{0}";
                    if (input.Contains(","))
                    {
                        format = "\"{0}\"";
                    }

                    if (i == table.Columns.Count - 1)
                    {
                        sb.Append(String.Format(format, ReplaceSpecialChars(input)));
                    }
                    else
                    {
                        sb.AppendFormat(format + ",", ReplaceSpecialChars(input));
                    }
                }

                if (index < table.Rows.Count - 1)
                    sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public static void ExportToCSVFile(DataTable table, string filename)
        {
            // using (StreamWriter sw = new StreamWriter(filename, false,Encoding.UTF8))
            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                string text = ExportToCSV(table);
                sw.WriteLine(text);
            }
        }
        public static DataTable ExcelDs(string filenameurl)
        {
            string strConn = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1;'", filenameurl); ;
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            //返回Excel的架构，包括各个sheet表的名称,类型，创建时间和修改时间等　
            DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            //包含excel中表名的字符串数组
            string[] strTableNames = new string[dtSheetName.Rows.Count];
            for (int k = 0; k < dtSheetName.Rows.Count; k++)
            {
                strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
            }


            OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + strTableNames[0] + "]", conn);
            DataTable ds = new DataTable(); odda.Fill(ds);

            conn.Close();
            conn.Dispose();
            return ds;
        }
        public static string ReplaceSpecialChars(string input)
        {
            // space -> _x0020_   特殊字符的替换
            // % -> _x0025_
            // # -> _x0023_
            // & -> _x0026_
            // / -> _x002F_
            if (input == null)
                return "";
            //input = input.Replace(" ", "_x0020_");
            //input.Replace("%", "_x0025_");
            //input.Replace("#", "_x0023_");
            //input.Replace("&", "_x0026_");
            //input.Replace("/", "_x002F_");

            input = input.Replace("\"", "\"\"");

            return input;
        }
    }

    /// <summary>
    /// sun : use ace.oledb, not use jet.oledb
    /// </summary>
    /// <param name="excelFilename"></param>
    /// <returns></returns>

}
