using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DBContext;
using WebApi.Fillter;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DemoController : ApiController
    {

        /// <summary>
        /// 获取自定义属性
        /// </summary>
        /// <returns></returns>
        [Route("api/attribute/test")]
        public string GetSql()
        {

            return Library.Sql.SqlHelper.Find<TestTables>(1);
        }


        /// <summary>
        /// 接口获取数据
        /// </summary>
        /// <returns></returns>
        [Route("api/book/list")]
        public List<Book> GetList()
        {

            WebApiDbcontext context = new WebApiDbcontext();
            List<Book> list = context.Books.ToList();

            return list;
        }
        [HttpPost, Route("api/book/add")]
        public string Add(Book book)
        {
            WebApiDbcontext context = new WebApiDbcontext();
            context.Books.Add(new Book()
            {
                book_auth = book.book_auth,
                book_name = book.book_name,
                book_price = book.book_price,
                 CreaTime = book.CreaTime,

            });
            context.SaveChanges();
            return "添加成功";
        }
        [HttpPost,Route("api/book/modify")]
        public string Modify(Book book)
        {
            WebApiDbcontext context = new WebApiDbcontext();
            Book oldBook = context.Books.FirstOrDefault(t => t.book_id == book.book_id);

            if (oldBook != null)
            {
                oldBook.book_auth = book.book_auth;
                oldBook.book_name = book.book_name;
                oldBook.book_price = book.book_price;
            }

            context.Entry<Book>(oldBook).State = EntityState.Modified;
            context.SaveChanges();

            return "修改成功";
        }

        [HttpPost, Route("api/book/delete")]
        public string Delete(int id)
        {
            WebApiDbcontext context = new WebApiDbcontext();
            Book oldBook = context.Books.FirstOrDefault(t => t.book_id == id);

            if (oldBook != null)
            {
                context.Books.Remove(oldBook);
            }
             
            context.SaveChanges();

            return "删除成功";
        }
    }
}
