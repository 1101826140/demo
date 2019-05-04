using ConsoleDemo.delegateUtil;
using ConsoleDemo.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            {
                //集合总汇
                ListDemo.Get();
            }

            {
                DelegateDemo.Get();
            }
            Console.ReadKey();
        }
    }
}
