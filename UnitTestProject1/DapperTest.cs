//==============================================================

//  作者：sun  (sunhlp@qq.com)

//  时间：2/3/2019 9:58:36 PM

//  文件名：Dapper

//  版本：V1.0.1  

//  说明： 

//  修改者：sun     
//  修改说明： 

//=================================================

using DapperLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class DapperTest1
    {
        [TestMethod]
        public void Test1()
        {

            TestTables person = new TestTables();

            //var t = person.Insert();
            //Console.WriteLine(person.Query().Count());

            //person.filterQuery().ForEach(p =>
            //{
            //    Console.WriteLine("名称：" + p.Name);
            //});

            Console.WriteLine(person.Update());
        }

        /// <summary>
        /// 程序集
        /// </summary>
        [TestMethod]
        public void Library()
        {
            namelist[0] = "123";
            namelist[1] = "1234";
            namelist[2] = "1235";
            namelist[3] = "1236";
            namelist[4] = "1237";
          

            Console.WriteLine(namelist[4]);
        }

       
        private string[] namelist = new string[5];

        public string this[int index]
        {
            get {
                string temp = "";
                //return "当前索引号" + index;
                if(namelist.Length<index && index >= 0)
                {
                    temp = namelist[index];

                }else
                {
                    temp = "";
                }
                return temp;

            }
            set
            {
                 

            }

        }



    }



}
