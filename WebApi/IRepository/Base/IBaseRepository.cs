using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Base.IRepository
{
    public interface IBaseRepository<T> : IDisposable where T : class, new()
    {
        int Insert(T instance);
        int Update(T instance);
        void Delete(object key);

        List<T> Query(params DbParameter[] paras);
    }
}
