using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorDemo.OperateUtil
{
    public class Jia : BaseOperate
    {
        public int Calculation(int left, int right)
        {
            return left - right;
        }
    }
}
