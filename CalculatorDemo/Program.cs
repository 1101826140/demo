using CalculatorDemo.Factory;
using CalculatorDemo.OperateUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorDemo
{
    class Program
    {
        static void Main(string[] args)
        {


            {

                //写以下代码目的：使用设计模式+ 反射 实现一个可扩展性的简易计算器
                Console.WriteLine("请输入一个int类型数值：");

                int left = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("请输入+-*/中的一个符号");
                string flag = Console.ReadLine();

                Console.WriteLine("请输入一个int类型数值：");
                int right = Convert.ToInt32(Console.ReadLine());

                int result = 0;

                OperateContext context = new OperateContext(left, right, flag);
                result = context.Calculation();

                Console.WriteLine("计算结果：");
                Console.WriteLine($"{result}");

                Console.WriteLine("请敲空格键结束。");


            }
            {

                //思考：算法这块：是否可以计算如下情况： 
                //公式一： (20000 / 365) *30
                //公式二： (12000/ 12) + (1000/10)
                //二叉树??
            }
            Console.ReadKey();
        }
    }
}
