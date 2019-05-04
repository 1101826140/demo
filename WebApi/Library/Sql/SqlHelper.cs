using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Library.Sql
{
    public class SqlHelper
    {
        public static string Find<T>(int id) where T : class
        {
            Type type = typeof(T);

            //版本一
            string columns = string.Join(",", type.GetProperties().Select(t => $"[{t.Name}] "));
            string sql = $"select {columns} from {type.Name} where id = {id} ";


            return sql;
        }
    }
}