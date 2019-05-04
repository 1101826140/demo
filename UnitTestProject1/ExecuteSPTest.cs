using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data;

namespace UnitTestProject1
{
    [TestClass]
    public class ExecuteSPTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataSet set = ExecStoredProcedure("Proc_Page", new SqlParameter[] {
                new SqlParameter("@StartIndex",1),
                new SqlParameter("@EndIndex",4)
            });

            int set2 = NotQueryExecStoredProcedure("Proc_Out", new SqlParameter[] {
                new SqlParameter("@Id",1),
                new SqlParameter("@gradeId",4)
            });
        }


  
        public DataSet ExecStoredProcedure(string procName, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection("Data Source=SUN;Initial Catalog=DemoTest;User ID=sa;Password=123456;"))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    SqlTransaction st = conn.BeginTransaction();
                    cmd.Transaction = st;
                    try
                    {
                        cmd.CommandText = procName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);
                        SqlDataAdapter ad = new SqlDataAdapter(cmd);

                        ad.Fill(ds);
                        st.Commit();
                        return ds;
                    }
                    catch (SqlException sqlex)
                    {
                        st.Rollback();
                        throw sqlex;
                    }
                }
            }
        }


        public int NotQueryExecStoredProcedure(string procName, params SqlParameter[] parameters)
        {
            int ds = 0;
            using (SqlConnection conn = new SqlConnection("Data Source=SUN;Initial Catalog=DemoTest;User ID=sa;Password=123456;"))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    SqlTransaction st = conn.BeginTransaction();
                    cmd.Transaction = st;
                    try
                    {
                        cmd.CommandText = procName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(parameters);

                        ds = cmd.ExecuteNonQuery();
                        st.Commit();
                        return ds;
                    }
                    catch (SqlException sqlex)
                    {
                        st.Rollback();
                        throw sqlex;
                    }
                }
            }
        }
    }
}
