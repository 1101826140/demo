using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class ThreadTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            BookShop shop = new BookShop();
            Thread thread = new Thread(new ThreadStart(shop.Sale));
            Thread thread2 = new Thread(new ThreadStart(shop.Sale));
            thread.Start();
            thread2.Start();
        }
        [TestMethod]
        public void Test2() {
            List<Task> taskList = new List<Task>();
            Console.WriteLine($"项目经理启动一个项目。。【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
            Console.WriteLine($"前置的准备工作。。。【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
            Console.WriteLine($"开始编程。。。【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
            taskList.Add(Task.Run(() => this.Coding("Tom", "Client")));
            taskList.Add(Task.Run(() => this.Coding("Jack", "Service")));
            // 等待集合中的所有线程都执行完
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine($"告诉甲方验收，上线使用【{Thread.CurrentThread.ManagedThreadId.ToString("00")}】");
        }

        /// <summary>
        /// 编码的方法
        /// </summary>
        /// <param name="name">开发人员Name</param>
        /// <param name="project">负责的模块</param>
        private void Coding(string name, string project)
        {
            Console.WriteLine($"****************Coding {name} Start {project} {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}***************");
            long lResult = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                lResult += i;
            }
            //Thread.Sleep(2000);

            Console.WriteLine($"****************Coding {name}   End {project} {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {lResult}***************");
        }
    }



    class BookShop
    {

        public int num = 1;
        public void Sale()
        {

            int tmp = num;
            if (tmp > 0)
            {
               // Thread.Sleep(1000);
                num -= 1;
            }

            Console.WriteLine("当前书籍数量：" + num);

        }

        public void Sale2()
        {

            lock (this)
            {
                int tmp = num;
                if (tmp > 0)
                {
                    Thread.Sleep(1000);
                    num -= 1;
                }

                Console.WriteLine("当前书籍数量：" + num);



            }
        }


    }
}
