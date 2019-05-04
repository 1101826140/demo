using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using WebFormDemo.Models;

namespace WebFormDemo.BLL
{
    public class BookBLL
    {
        public static DataTable GetList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=DemoTest;Integrated Security=True"))
            {
                conn.Open();
                SqlDataAdapter cmd = new SqlDataAdapter("select * from [books] ", conn);
                cmd.Fill(dt);
                conn.Close();
            }
            return dt;

        }

        public static object GetBookList(int pageindex = 1, int pagesize = 10,
            string sort = "book_id", string order = "desc")
        {
            int total = 0;
            if (pageindex <= 0)
            {
                pageindex = 1;
            }
            if (pagesize <= 0)
            {
                pagesize = 10;
            }
            if (string.IsNullOrEmpty(sort))
            {
                sort = "book_id";
            }
            if (string.IsNullOrEmpty(order))
            {
                order = "desc";
            }

            //验证参数不做处理了
            string sql = $@"
 select * from
(
select * , row_number() over ( order by {sort} {order} ) as rownum
from books
) Tmp
where Tmp.rownum >{(pageindex - 1) * pagesize} and Tmp .rownum<= {pageindex * pagesize}
";

            List<Book> rows = new List<Book>();
            using (SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=DemoTest;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    rows.Add(new Book() //这里为空的情况暂时不处理了
                    {
                        book_auth = dr["book_auth"].ToString(),
                        book_id = Convert.ToInt32(dr["book_id"].ToString()),
                        book_name = dr["book_name"].ToString(),
                        book_price = Convert.ToDecimal(dr["book_price"].ToString()),
                    });
                }
                dr.Close();
                
                string sqlcount = "select count(*) from books ";
                SqlCommand cmd2 = new SqlCommand(sqlcount.ToString(), conn);
                total = Convert.ToInt32(cmd2.ExecuteScalar());

            }
            return new
            {
                rows = rows,
                total = total,
            };
        }
    }
}