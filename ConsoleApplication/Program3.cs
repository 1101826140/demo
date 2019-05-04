using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// sealed类表示该类不能被继承
/// </summary>
namespace ConsoleApplication
{
    public class Program3
    {

        public static void Main1(string[] args)
        {

            ThreadTest2 test = new ThreadTest2();
            Thread t = new Thread(test.GetUser);
            Thread t1 = new Thread(test.GetUser2);

            t.Start();
            t1.Start();
            Console.ReadKey();
        }

    }

    public class ThreadTest2
    {

        object user1 = new object();
        object user2 = new object();

        public void GetUser()
        {

            lock (user1)
            {


                lock (user2)
                {

                    Console.WriteLine("111111");
                }
            }
        }

        public void GetUser2()
        {

            lock (user2)
            {


                lock (user1)
                {

                    Console.WriteLine("222");
                }
            }
        }
    }

}
