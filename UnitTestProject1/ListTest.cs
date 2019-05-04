using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace UnitTestProject1
{
    [TestClass]
    public class ListTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] values = { 3, 5, 9, 8, 10, 5, 3 };
            HashSet<int> set = new HashSet<int>();
            foreach (int i in values)
            {
                set.Add(i);
            }
            Console.WriteLine(set.Count);


            HashSet<int> set2 = new HashSet<int>(values);

        }

        [TestMethod]
        public void StringAndEquals()
        {
            String test = "sun";
            string a = "sun";
            string b = "sun";


            string st1, st2;
            st1 = "hello";
            st2 = st1;
            st1 = "world";

            Console.WriteLine("s2:" + st2);
            if (test == b)
            {
                Console.WriteLine("test=b");
            }
            else
            {

                Console.WriteLine("a!=b");
            }

            if (a.Equals(b))
            {
                Console.WriteLine("a equals b");
            }
            else
            {
                Console.WriteLine("a not equals b");
            }

        }


        [TestMethod]
        public void TestSun()
        {
            B obj = new B();

            obj.PrintFields();
        }
    }

    class A
    {
        public A()
        {
            PrintFields();
        }
        public virtual void PrintFields() { }
    }
    class B : A
    {
        int x = 1;
        int y;
        public B()
        {
            y = -1;
        }
        public override void PrintFields()
        {
            Console.WriteLine("x={0},y={1}", x, y);
        }

    }
}
