using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace UnitTestProject1
{
    [TestClass]
    public class SqlServerTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            
        }
        public static DataTable ExcuteDataTable(string sql, string con)
        {
            var cmd = new SqlCommand();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    connection.Open();
                    var tran = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                    var da = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                var dt = new DataTable();
                dt.Columns.Add("异常信息");
                DataRow row = dt.NewRow();
                row["异常信息"] = ex.Message;
                dt.Rows.Add(row);
                return dt;
            }
        }
 
        
    }
}
