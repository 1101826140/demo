using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    /// <summary>
    /// c#7.0语法
    /// </summary>
    [TestClass]
    public class C7Test
    {
        [TestMethod]
        public void TestMethod1()
        {

            //out 和 ref 的区别 ： ref参数必须初始化，out参数不需要初始化 
            string name;
            int age;
            TestOut(out name, out age);

            string sex = "";
            TestRef(ref sex);

            object a = new object(), b = new object(), c = new object();
            Test1(out a);
            Test2(ref b);
            Test3(c);


            //  this.TestOut(out string name2, out int age2);
        }

        public void TestOut(out string name, out int age)
        {

            name = "sun";
            age = 26;
        }

        public void TestRef(ref string sex)
        {

            sex = "女";
        }

        public void Test1(out object a)
        {

            a = null;
        }

        public void Test2(ref object a)
        {

            a = null;
        }

        public void Test3(object a)
        {

            a = null;
        }



        [TestMethod]
        public void FunTest()
        {
            //内置泛型委托
            Func<int, int> method = new Func<int, int>(GetAge);
            int age = method(26);

            //使用委托
            Func<int, int> method2 = delegate (int i)
               {
                   return i;
               };
            int age2 = method2(18);

            //匿名委托
            Func<int, int> method3 = (int i) =>
              {

                  return i;
              };

            int age3 = method3(13);


            //匿名委托
            Func<int, string, bool> method4 = (int value1, string value2) =>
            {

                return value1 == Convert.ToInt32(value2);
            };

            bool isSame = method4(12, "12");

        }
        public int GetAge(int age)
        {
            return age;
        }
    }
}
