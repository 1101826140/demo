using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.delegateUtil
{
    public static class DelegateDemo
    {
        public delegate string Query(string para1, int para2);

        public delegate string Select(string p1, string p2, string p3, string p4, string p5, string p6,
            string p7, string p8, string p9, string p10, string p11, string p12,
            string p13, string p14, string p15, string p16, string p17, string p18);
        public static void Get()
        {
            Console.WriteLine("********************************");

            {
                //.net framework 1.0
                Query method = new Query(QueryUser);
                var value = method.Invoke("sun", 10);
                Console.WriteLine(value);//
            }

            {
                //2.0
                Query method = new Query(delegate (string para1, int para2)
                {

                    return "返回用户：" + para1 + ", 年龄：" + para2;
                });

                string value = method.Invoke("sun", 20);
                Console.WriteLine(value);
            }

            {
                //3.0
                Query method = new Query((string para1, int para2) =>
                {
                    return "返回用户：" + para1 + ", 年龄：" + para2;
                });
                string value = method.Invoke("sun", 30);
                Console.WriteLine(value);
            }

            {
                //c#内置委托，带有一个string参数的，无返回值的匿名方法
                Action<string> method = (string para1) =>
                 {
                     Console.WriteLine("hello " + para1 + " , age : 40");
                 };
                method.Invoke("sun");
            }
            {

                //Action 最多16个参数
                Action<string, string, string, string, string,
                       string, string, string, string, string,
                       string, string, string, string, string,
                       string> method = (p1, p2, p3, p4, p5,
                                        p6, p7, p8, p9, p0,
                                        pq, pw, pe, pr, pp, pl) =>
                       {
                           Console.WriteLine("这里就不输出16个参数的值了");
                       };
                method.Invoke("1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "17", "18");
            }
            {

                //扩展 17个参数如何设置
                //自己声明一个带有17个参数的委托
                Select method = (p1, p2, p3, p4, p5,
                                        p6, p7, p8, p9, p10,
                                        p11, p12, p13, p14, p15, p16, p17, p18) =>
                {

                    return "这里是带有18个参数的值";
                };
                var value = method.Invoke("1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "17", "18");
                Console.WriteLine(value);
            }

            {
                //内置委托，有参
                Func<int, string> method = (id) =>
                {

                    return "我通过传递" + id + ",返回string的值";
                };
                var value = method.Invoke(50);
                Console.WriteLine(value);
            }

            {
                //linq
                User u = UserList().Find(t => t.Id == 1);
                Console.WriteLine("linq自带的方法：" + u.Name);

                //自己定义一个
                User tmp = UserList().FindByDemo(t => t.Id == 1);
                Console.WriteLine("自己手写方法:" + tmp.Name);


                //思考 场景:查询语句会出现多条件的查询（面试中有被问到），如果做到不通过if else判断
                IEnumerable<User> list = null;
                List<User> datasource = UserList();
                if (1 == 0) //假设过滤条件为用户年纪大于20
                {
                    list = datasource.Where(t => t.Age > 20);
                }

                if (1 == 1) //假设过滤条件为用户名字包含i的用户
                {
                    list = datasource.Where(t => t.Name.IndexOf("i") >= 0);
                }

                //以上场景写法

                //合理写法：

            }
            {
                //linq to object 之IEnumerable 操作内存     使用内置委托
                //linq to sql    之 IQueryable 拼接sql??    表达式目录树，二叉树
            }
            Console.WriteLine("********************************");
        }


        private static string QueryUser(string Name, int age)
        {
            return "返回用户：" + Name + ", 年龄：" + age;
        }


        private static List<User> UserList()
        {



            List<User> tmp = new List<User>();

            tmp.Add(new User() { Id = 1, Age = 10, Name = "sun" });
            tmp.Add(new User() { Id = 2, Age = 20, Name = "bob" });
            tmp.Add(new User() { Id = 3, Age = 25, Name = "justin" });
            tmp.Add(new User() { Id = 4, Age = 30, Name = "linda" });
            tmp.Add(new User() { Id = 5, Age = 40, Name = "lucy" });
            tmp.Add(new User() { Id = 6, Age = 50, Name = "momo" });
            tmp.Add(new User() { Id = 7, Age = 60, Name = "any" });
            tmp.Add(new User() { Id = 8, Age = 70, Name = "lily" });

            return tmp;

        }

        public static T FindByDemo<T>(this List<T> list, Func<T, bool> expression) where T : class, new()
        {
            T t = new T();
            foreach (var u in list)
            {
                if (expression.Invoke(u))
                {
                    t = u;
                }
            }

            return t;
        }

    }

    public class User
    {

        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
