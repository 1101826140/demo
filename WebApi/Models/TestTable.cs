using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    /// <summary>
    /// 自己手写属性
    /// </summary>
    [Library.Attributes.SunTable("sun_TestTables")]
    public class TestTables
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }
}