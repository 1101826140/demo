﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DBContext;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Controllers.v1
{
    [VersionedRoute("api/version", 1)]
    public class OrderController : ApiController
    {
        /// <summary>
        /// 添加订单
        /// </summary>
        [HttpPost, Route("sun/test/order/add1")]
        //public void Add(OrderAddArgs args)
        //{
        //    OrderRepository repository = new OrderRepository();
        //    repository.Insert(new Order()
        //    {
        //        OrderDate = DateTime.Now,
        //        OrderId = args.OrderID,
        //        Price = args.Price,
        //    });

        //}
        public void Add()
        {

            using (var context = new WebApiDbcontext())
            {


                var isCreate = context.Database.CreateIfNotExists();
            }
        }

        //[HttpGet, Route("v1/")]
        //public void IsCreateDB
        //{


        //}
    }

    public class OrderAddArgs
    {
        public string OrderID { get; set; }

        public decimal? Price { get; set; }
    }
}