using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace UnitTestProject1
{

    /// <summary>
    /// 单例模式
    /// </summary>
    [TestClass]
    public class Singleton
    {
        [TestMethod]
        public void TestMethod1()
        {

            //这种情况只会创建一次
            //SingletonDemo demo = SingletonDemo.CreateInstance();
            //SingletonDemo demo2 = SingletonDemo.CreateInstance();

            //程序执行异步调用 对象可能会被初始化多次
            //for (int i = 0; i < 100; i++)
            //{
            //    new Action(() =>
            //    {
            //        SingletonDemo demo = SingletonDemo.CreateInstance();
            //    }).BeginInvoke(null, null);
            //} 

            //程序执行异步调用 对象可能会被初始化多次
            //for (int i = 0; i < 100; i++)
            //{
            //    new Action(() =>
            //    {
            //        SingletonDemo demo = SingletonDemo.CreateInstance2();
            //    }).BeginInvoke(null, null);
            //}

            ////静态构造方法创建
            //SingletonDemo demo = SingletonDemo.CreateInstance3();
            SingletonDemo demo = SingletonDemo.CreateInstance4();
        }

        sealed class SingletonDemo
        {
            /// <summary>
            /// 私有构造函数
            /// </summary>
            private SingletonDemo()
            {
                 Trace.WriteLine("i am init"); 
            }


            private static SingletonDemo Instance;

            public static SingletonDemo CreateInstance()
            {

                if (Instance == null)
                {
                    Instance = new SingletonDemo();
                }

                return Instance;
            }


            private static object Singleton_Lock = new object();
            public static SingletonDemo CreateInstance2()
            {

                lock (Singleton_Lock)
                {

                    if(Instance == null)
                    {
                        Instance = new SingletonDemo();
                    }
                }

                return Instance;
            }

            /// <summary>
            ///  静态构造函数：由CLR保证在第一次使用这个类之前，调用而且只调用一次
            /// </summary>
            static SingletonDemo()
            {
                Instance = new SingletonDemo();
            }

            public static SingletonDemo CreateInstance3()
            {

                return Instance;
            }

            private static SingletonDemo instance2 = new SingletonDemo();
            public static SingletonDemo CreateInstance4()
            {
                return instance2;
            }
        }
    }
}
