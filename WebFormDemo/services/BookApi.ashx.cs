using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using WebFormDemo.BLL;
using WebFormDemo.Models;

namespace WebFormDemo.services
{
    /// <summary>
    /// Summary description for BookApi
    /// </summary>
    public class BookApi : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string method = context.Request.QueryString["method"].ToString();
            switch (method)
            {
                case "GetList":
                    GetList(context);
                    break;


            }
        }

        public void GetList(HttpContext context)
        {

            string sort = context.Request.Form["sort"];
            string order = context.Request.Form["order"];
            int pageindex = 1; int.TryParse(context.Request.Form["page"], out pageindex);
            int pagesize = 10; int.TryParse(context.Request.Form["rows"], out pagesize);
            var table = BookBLL.GetBookList(pageindex, pagesize, sort, order);


            //var table = "[{\"book_id\":1,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":2,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":3,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":4,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":5,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":6,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":7,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":8,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":9,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":10,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":11,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"},{\"book_id\":12,\"book_name\":\"图书十1\",\"book_price\":210,\"book_auth\":\"sun111\"}]";
            context.Response.Write(JsonConvert.SerializeObject(table));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}