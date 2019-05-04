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
    public class Program2
    {
        public static void showInt(int paras)
        {
            Console.WriteLine("{0}-{1}", paras.GetType().Name, paras);

        }
        public static void showObj(object paras)
        {
            Console.WriteLine("{0}-{1}", paras.GetType().Name, paras);
        }

        public static void showObj2(object paras)
        {
            Console.WriteLine("{0}-{1}", paras.GetType().Name, paras);
            Console.WriteLine($"{((People)paras).Id}__{((People)paras).Name}");
        }
        /// <summary>
        /// 泛型方法 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        public static void show<T>(T paras)
        {
            Console.WriteLine("{0}-{1}", paras.GetType().Name, paras);
        }

        /// <summary>
        /// 泛型约束方法，必须继承people才能使用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paras"></param>
        public static void show2<T>(T paras) where T : People
        {
            Console.WriteLine("{0}-{1}", paras.Id, paras.Name);
            paras.Hi();
        }

        static void Main2(string[] args)
        {
            #region IDisposable
            Stopwatch sw = new Stopwatch();

            TestDisponsable test = new TestDisponsable();
            try
            {
                test.DoSomething();
            }
            finally
            {
                test.Dispose();
            }

            #endregion


            #region 泛型方法测试

            showInt(1);
            showObj(1);
            show<int>(1);

            string result = string.Empty;
            int count = 1;
            //测量一个时间间隔的运行时间  ElapsedMilliseconds 毫秒
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                showInt(1111);
            }
            watch.Stop();
            var t1 = watch.ElapsedMilliseconds;

            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                showObj(1111);
            }
            watch.Stop();
            var t2 = watch.ElapsedMilliseconds;

            watch.Restart();
            for (int i = 0; i < count; i++)
            {
                show<int>(1111);
            }
            watch.Stop();
            var t3 = watch.ElapsedMilliseconds;

            Console.WriteLine($"{t1}-{t2}-{t3}");

            #endregion


            #region 泛型类测试

            BaseClas<int> baseInt = new BaseClas<int>();
            baseInt._T = 123;

            BaseClas<string> baseString = new BaseClas<string>();
            baseString._T = "sun";

            #endregion


            #region 泛型接口

            #endregion


            #region  约束泛型

            People people = new People()
            {

                Id = 1,
                Name = "people"
            };
            Chinese cn = new Chinese()
            {
                Id = 1,
                Name = "Chinese",
            };

            Anhui ah = new Anhui()
            {
                Id = 1,
                Name = "anhui",
            };

            Japen japen = new Japen()
            {
                Id = 1,
                Name = "japen"
            };

            //输出id 和 name
            //showObj2(people);
            //showObj2(cn);
            //showObj2(ah);
            //showObj2(japen); //会出错


            show2(people);
            show2(cn);
            show2(ah);
            //show2(japen);



            Animal animal = new Animal();
            animal.Name = "动物";
            Dog dog = new Dog();
            dog.Name = "狗";
            dog.age = 2;
            Animal dog2 = new Dog();
            //List<Animal> list = new List<Dog>();

            //协变
            IEnumerable<Animal> list = new List<Dog>();

            Func<Animal> func = new Func<Dog>(() => null);

            IBaseClass2Out<Animal> list2 = new BaseClass2<Dog>();
            Console.ReadKey();
            #endregion
        }


        static void Main3(string[] args)
        {
            BookShop book = new BookShop();


            ////创建两个线程同时访问Sale方法
            //Thread t1 = new Thread(new ThreadStart(book.Sale));
            //Thread t2 = new Thread(new ThreadStart(book.Sale));
            //Thread t3 = new Thread(book.Sale);

            ////启动线程
            //t1.Start();
            //t2.Start();
            //t3.Start();

            //ThreadTest test = new ThreadTest();
            //Thread t = new Thread(test.GetUser);
            //Thread t1 = new Thread(test.GetUser2);

            //t.Start();
            //t1.Start();
            //Console.ReadKey();

            //前台线程 后台线程

            //演示前台、后台线程
            BackGroundTest background = new BackGroundTest(1000000);
            //创建前台线程
            Thread fThread = new Thread(new ThreadStart(background.RunLoop));
            //给线程命名
            fThread.Name = "前台线程";


            BackGroundTest background1 = new BackGroundTest(1000000);
            //创建后台线程
            Thread bThread = new Thread(new ThreadStart(background1.RunLoop));
            bThread.Name = "后台线程";
            //设置为后台线程
            bThread.IsBackground = true;

            //启动线程

            bThread.Start();
            fThread.Start();
            Console.ReadKey();

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
                //Thread.Sleep(1000);
            }
            Console.WriteLine("{0}完成计数", threadName);

        }
    }

    class BookShop
    {
        //剩余图书数量
        public int num = 1;
        public void Sale()
        {
            lock (this)
            {


                int tmp = num;
                if (tmp > 0)//判断是否有书，如果有就可以卖
                {
                    Thread.Sleep(1000);
                    num -= 1;
                    Console.WriteLine("售出一本图书，还剩余{0}本", num);
                }
                else
                {
                    Console.WriteLine("没有了");
                }
            }
        }
    }

    public class TestDisponsable : IDisposable
    {
        public void DoSomething()
        {
            Console.WriteLine("dosomething");
        }
        public void Dispose()
        {
            Console.WriteLine("释放资源");
        }
    }

    public interface IBaseClass2Out<out T>
    {
        T Get();
    }
    public class BaseClass2<T> : IBaseClass2Out<T>
    {
        public T Get()
        {
            throw new NotImplementedException();
        }
    }

    public class BaseClas<T>
    {
        public T _T;
    }

    public interface IGenericInterface<T>
    {
        T GetT(T t);
    }


    public class Student : IGenericInterface<string>
    {
        public string ID { get; set; }

        public string GetT(string t)
        {
            throw new NotImplementedException();
        }
    }

    public class CommonClass<T> : IGenericInterface<T>
    {
        public T GetT(T t)
        {
            throw new NotImplementedException();
        }
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Hi()
        {
            Console.WriteLine("Hi");
        }
    }

    public class Chinese : People, ISports, IWork
    {
        public void Pingpan()
        {
            Console.WriteLine("打乒乓球...");
            int i = 0;

        }

        public void work()
        {
            throw new NotImplementedException();
        }

        public void SayHi()
        {
            Console.WriteLine("你好啊");
        }
    }

    public class Anhui : Chinese
    {
        public void SayHi()
        {
            Console.WriteLine("吃了吗");
        }


    }

    public class Japen : ISports
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Pingpan()
        {
            throw new NotImplementedException();
        }
    }

    public class Animal
    {

        public string Name { get; set; }
    }

    public class Dog : Animal
    {

        public int age { get; set; }
    }
    public class ThreadTest
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
