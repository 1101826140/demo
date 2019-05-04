using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo.List
{

    /// <summary>
    ///  基础回顾：集合：
    ///  1：线性结构  一对一关系
    ///  2：树形结构  一对多
    ///  3:图状结构  多对多
    /// </summary>
    public class ListDemo
    {
        public static void Get()
        {
            #region 线性结构
            {

                {
                    /*
                     线性结构1:\
                     缺陷: 长度要指定，同理string[]
                     优点：内存连续存储 节约空间，可以索引访问,读取速度快，增删慢                    
                     */
                    int[] list = new int[10];
                    list[0] = 5;
                    list[1] = 2;
                    list[2] = 0;

                }
                {
                    /*
                        线性结构1:\
                        缺陷: 将 int[],string[]转化为泛型,装箱拆箱增加性能损耗
                        优点：内存连续存储 节约空间，可以索引访问,读取速度快，增删慢                    
                    */
                    ArrayList list = new ArrayList();
                    list.Add("sun");
                    list.Add("say");
                    list.Add("hello");
                    list.Add(DateTime.Now);
                }
            }
            #endregion

            #region 链表结构
            {
                /*
                 链表结构
                 单链表，双向链表，循环链表
                 存储格式：数据+地址
                 缺点：读取慢，增加了存储空间
                 优点：增删快
                 */
                List<string> list = new List<string>();//使用泛型过程中制定了格式
                list.Add("hello");
                list.Add("shang hai");
            }

            {
                //先进先出
                Queue<string> list = new Queue<string>();
                list.Enqueue("are"); //入队
                list.Enqueue("you");
                list.Enqueue("ok");

                Console.WriteLine(string.Join(",", list));  //are,you,ok

                list.Dequeue(); //出队
                Console.WriteLine(string.Join(",", list));  //you,ok

                //获取队列头部元素，不做移除动作
                string value = list.Peek();//you
                Console.WriteLine(value);

                string value1 = list.Peek();//you
                Console.WriteLine(value1);
            }
            {
                //栈
                Stack<string> list = new Stack<string>();
                list.Push("易烊千玺");
                list.Push("崇拜");
                list.Push("我");
                Console.WriteLine(string.Join(",", list));  //我,崇拜,易烊千玺

                list.Pop();
                Console.WriteLine(string.Join(",", list));  //崇拜,易烊千玺

                string value = list.Peek();
                Console.WriteLine(value);  //崇拜

                //支持重复
                list.Push("崇拜");
                Console.WriteLine(string.Join(",", list));  //崇拜,崇拜,易烊千玺

            }

            {
                //排重，唯一性，IP投票 统计用户id等
                HashSet<string> list = new HashSet<string>();
                list.Add("啊");
                list.Add("啊");
                list.Add("啊");
                list.Add("我看到明星了！");
                //HashSet:啊,我看到明星了！   长度:2
                Console.WriteLine("HashSet:" + string.Join(",", list) + "   长度:" + list.Count);

            }
            {
                //排重，唯一性，IP投票 统计用户id等
                SortedSet<string> list = new SortedSet<string>();
                list.Add("啊");
                list.Add("啊");
                list.Add("啊");
                list.Add("我看到明星了！");

                //SortedSet: 啊,我看到明星了！   长度: 2
                Console.WriteLine("SortedSet:" + string.Join(",", list) + "   长度:" + list.Count);
            }
            {
                //自动排序
                SortedSet<int> list = new SortedSet<int>();
                list.Add(11);
                list.Add(11);
                list.Add(9);
                list.Add(20);

                //SortedSet：我期待的结果是自动排序了：9,11,20   长度: 3
                Console.WriteLine("SortedSet：我期待的结果是自动排序了：" + string.Join(",", list) + "   长度:" + list.Count);
            }
            {
                //增删都快的,用空间换性能
                Hashtable list = new Hashtable();

                list.Add("name", "sun");
                list.Add("age", 18);
                //list.Add("age", 19);//新增相同key会报错

                Console.WriteLine(string.Join(",", list.Keys.Count)); //2

                list.Remove("age");
                Console.WriteLine(string.Join(",", list.Keys.Count));//1

                bool IsExistName = list.Contains("name");
                bool IsExistNa = list.Contains("na");
                //True-False
                Console.WriteLine(IsExistName + "-" + IsExistNa);
            }
            #endregion


            {
                //IEnumerable 使用的时候linq to object方式
               
                Console.WriteLine("_________________________");
                MyColor colors = new MyColor();
                foreach (string c in colors)
                {
                    Console.WriteLine("color is : " + c);
                }
                  
                //ABC[] list = new ABC[10];
                //list.Add
                //foreach(var a in list)
                //{
                //    Console.WriteLine("ABC is :"+ a.Name); 
                //}


                int[] myArray = { 1, 32, 43, 343 };
                IEnumerator myie = myArray.GetEnumerator();
                myie.Reset();
                while (myie.MoveNext())
                {
                    int i = (int)myie.Current;
                    Console.WriteLine("Value: {0}", i);
                }


                /*
              
                
                 延时执行： IQueryable，IEnumberalb 为延时执行（用到的时候再查），IList一次性加载
                 顺时执行： IList一次性查询后加载到内存
                 IQueryable接口与IEnumberable接口的区别：  
                 IEnumerable<T> 泛型类在调用自己的SKip 和 Take 等扩展方法之前数据就已经加载在本地内存里了，
                 IQueryable<T>  是将Skip ,take 这些方法表达式翻译成T-SQL语句之后再向SQL服务器发送命令，
                                它并不是把所有数据都加载到内存里来才进行条件过滤。


                 */


                //IQueryable 生成sql 采用表达式目录树，二叉树查找
                //IQeurable（IQuerable<T>）:不在内存加载持久数据,因为这家伙只是在组装SQL，(延迟执行) 到你要使用的时候，
                //例如 list.Tolist() or list.Count()的时候，数据才从数据库进行加载(AsQueryable())。
                //IQueryable<CustomData> list2 = dbcontext.CustomDataList.Where(t => t.PrimaryDataID == "123");

                //IEnumberalb，使用的是LINQ to Object方式  内置委托,它会将AsEnumerable()时对应的所有记录都先加载到内存
                //，然后在此基础上再执行后来的Query
                //IEnumerable<CustomData> list2 = dbcontext.CustomDataList.Where(t => t.PrimaryDataID == "123").AsEnumerable();


                // IList<CustomData> list2 = dbcontext.CustomDataList.Where(t => t.PrimaryDataID == "123").ToList();



                //List:IList:ICollection:IEnumberable
                Console.WriteLine("_________________________");
            }
        }
    }
    public class ABC
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// 实现了IEnumberable接口也可以
    /// </summary>
    public class MyColor : IEnumerable
    {
        string[] colors = { "red", "white", "black", "yellow" };
        public IEnumerator GetEnumerator()
        {
            // throw new NotImplementedException();
            return colors.GetEnumerator();
        }
    }
}
