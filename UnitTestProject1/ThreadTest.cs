using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Diagnostics;

namespace UnitTestProject1
{
    /// <summary>
    /// 线程
    /// </summary>
    [TestClass]
    public class ThreadTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //创建线程
            //Thread thread = new Thread(new ThreadStart(method));
            ////启动线程
            //thread.Start();

            //Thread thread2 = new Thread(delegate ()
            //{
            //    Console.WriteLine("测试");
            //});
            //thread2.Start();


            //ParameterizedThreadStart  委托的参数带有object类型的方法
            //Thread thread3 = new Thread(new ParameterizedThreadStart(method2));
            //thread3.Start("sun");
            ////当前线程的唯一标识符
            //int id = thread3.ManagedThreadId;
            //thread3.Name = "主线程";
            //ThreadPriority priority = thread3.Priority; 
            //Console.WriteLine($"{id}---{priority}---{thread3.ThreadState}---{thread3.Name}");

            // 15-- - Normal-- - Running-- - 主线程
            // sun



            //前台线程 后台线程

            //演示前台、后台线程
            BackGroundTest background = new BackGroundTest(10);
            //创建前台线程
            Thread fThread = new Thread(new ThreadStart(background.RunLoop));
            //给线程命名
            fThread.Name = "前台线程";


            BackGroundTest background1 = new BackGroundTest(20);
            //创建后台线程
            Thread bThread = new Thread(new ThreadStart(background1.RunLoop));
            bThread.Name = "后台线程";
            //设置为后台线程
            bThread.IsBackground = true;

            //启动线程
            fThread.Start();
            bThread.Start();
        }

        public void method()
        {
            //Trace.WriteLine("i am method");
            Console.WriteLine("i am method");
        }

        public void method2(object value)
        {
            Console.WriteLine(value);
        }
    }


    class BackGroundTest
    {
        private int Count;
        public BackGroundTest(int count)
        {
            this.Count = count;
        }
        public void RunLoop()
        {
            //获取当前线程的名称
            string threadName = Thread.CurrentThread.Name;
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("{0}计数：{1}", threadName, i.ToString());
                //线程休眠500毫秒
                Thread.Sleep(1000);
            }
            Console.WriteLine("{0}完成计数", threadName);

        }
    }
}
