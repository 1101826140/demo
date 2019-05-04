using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Base.IRepository;

namespace WebApi.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class,new()
    {
        protected DbContext _context;
        protected DbSet<T> _dbset = null;

        public BaseRepository(DbContext context)
        {
            _context = context;
            this._dbset = this._context.Set<T>();

        }
        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns>
        public void Delete(object key)
        {
            if (key != null)
            {

                T instance = this._dbset.Find(key);
                if (instance != null)
                {

                    this._dbset.Remove(instance);
                }

                this._context.SaveChanges();
            }
        }

        public virtual void Dispose()
        {

            if (this._context != null)
            {
                this._context.Dispose();
            }
        }


        public int Insert(T instance)
        {
            int i = 0;
            if (instance != null)
            {
                this._dbset.Add(instance);
                this._context.SaveChanges();
            }
            return i;
        }

        public virtual List<T> Query(params DbParameter[] paras)
        {
            throw new NotImplementedException();
        }

        public virtual int Update(T instance)
        {
            int effectCount = 0;

            if (instance != null)
            {
                _dbset.Attach(instance);
                this._context.Entry(instance).State =  EntityState.Modified;
                this._context.SaveChanges();
            }

            return effectCount;
        }
    }
}

/*
 virtual :基类实现该方法，派生类无该方法，会默认调用基类方法
          ：可使用override 重写基类方法
     
     */
