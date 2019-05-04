using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class SortTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] data = new[] { 1, 3, 6, 2, 0 };
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = data.Length - 1; j > i; j--)
                {
                    if (data[j] > data[j - 1])
                    {
                        data[j] = data[j] + data[j - 1];
                        data[j - 1] = data[j] - data[j - 1];
                        data[j] = data[j] - data[j - 1];
                    }
                } 
            }
            Console.WriteLine(string.Join(",", data));
        }
        [TestMethod]
        public void TestPaixu()
        {
            PopSort(new[] { 1, 4, 2, 0, 3, 9 });
        }
        public void PopSort(int[] list)
        {
            int i, j, temp;  //先定义一下要用的变量
            for (i = 0; i < list.Length - 1; i++)
            {
                for (j = i + 1; j < list.Length; j++)
                {
                    if (list[i] < list[j]) //如果第二个小于第一个数
                    {
                        //交换两个数的位置，在这里你也可以单独写一个交换方法，在此调用就行了
                        temp = list[i]; //把大的数放在一个临时存储位置
                        list[i] = list[j]; //然后把小的数赋给前一个，保证每趟排序前面的最小
                        list[j] = temp; //然后把临时位置的那个大数赋给后一个
                    }
                }
            }
            Console.WriteLine(string.Join(",",list));
        }
        [TestMethod]
        public void Test()
        {

            int[] list = new[] { 2,1,35,6,3,221,5};
            for(int i = 0; i < list.Length; i++)
            {
                for (int j = i + 1; j < list.Length; j++)
                {
                    if (list[j] > list[i])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            Console.WriteLine(string.Join(",",list));
          
        }


        //冒泡排序
        public void BubbleSort(double[] data)
        {
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = data.Length - 1; j > i; j--)
                {
                    if (data[j] > data[j - 1])
                    {
                        data[j] = data[j] + data[j - 1];
                        data[j - 1] = data[j] - data[j - 1];
                        data[j] = data[j] - data[j - 1];
                    }
                }
            }
        }
    }
}
