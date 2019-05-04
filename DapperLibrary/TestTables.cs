
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperLibrary
{
    public class TestTables
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }


        public static string connectionString = "Data Source=SUN;Initial Catalog=WebApiDB;User ID=sa;Password=123456;MultipleActiveResultSets=True; Max Pool Size = 512;";
        public int Insert()
        {
            IDbConnection connection = new SqlConnection(connectionString);
            var result = connection.Execute(
                "insert into TestTables(Name,Email) values (@Name,@Email)",
                new
                {
                    Name = "sun",
                    Email = "sunhlp@qq.com",
                }
                );
            return result;
        }


        public int InsertBulk()
        {
            int result = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {

                var list = Enumerable.Range(1012, 10000).Select(i => new TestTables()
                {
                    Email = i + "qq.com",
                    Name = "韩丽萍" + i,
                });
                result = connection.Execute("insert into TestTables(Name,Email) values (@Name,@Email)", list);
            }
            return result;
        }

        public List<TestTables> Query()
        {
            IDbConnection connection = new SqlConnection(connectionString);
            var result = connection.Query<TestTables>("select *  from TestTables").ToList();
            return result;
        }

        public int Update()
        {
            int result = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {

           
                result = connection.Execute("update TestTables set Name=@Name where ID=@IDe", new {
                    Name="1260634",
                    Ide=1,//这个是参数的名称，且可以不区分大小写
                });
            }
            return result;
        }

        public List<TestTables> filterQuery()
        {

            IDbConnection connection = new SqlConnection(connectionString);
            List<TestTables> result = connection.Query<TestTables>("select *  from TestTables where ID in @ID ", new
            {
                ID = new int[] { 1, 2 }
            }

            ).ToList();
            return result;
        }
    }


}
