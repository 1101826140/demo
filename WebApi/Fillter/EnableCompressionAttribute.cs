using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace WebApi.Fillter
{
    /// <summary>
    /// 启用Gzip压缩
    /// </summary>
    public class EnableCompressionAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request == null || request.IsAjaxRequest())
            {
                return;
            }
            HttpResponseBase response = filterContext.HttpContext.Response;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (String.IsNullOrEmpty(acceptEncoding))
            {
                return;
            }
            if (acceptEncoding.ToLower().Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.ToLower().Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }
        }
    }
}