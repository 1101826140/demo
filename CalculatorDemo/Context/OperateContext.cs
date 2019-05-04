using CalculatorDemo.OperateUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorDemo.Factory
{
    public class OperateContext
    {
        public int left { get; private set; }
        public int right { get; private set; }
        public string flag { get; private set; }

        Dictionary<string, string> configList = new Dictionary<string, string>()
        {
            { "+","CalculatorDemo.OperateUtil.Jia"},
            { "-","CalculatorDemo.OperateUtil.Jian"},
            { "*","CalculatorDemo.OperateUtil.Chen"},
            { "/","CalculatorDemo.OperateUtil.Chu"},
        };

        public OperateContext(int left, int right, string flag)
        {
            this.left = left;
            this.right = right;
            this.flag = flag;
        }

        public BaseOperate CreateOperate(string flag)
        {
            Assembly assembly = Assembly.Load(configList[flag]);

            Type type = assembly.GetType();

            return (BaseOperate)Activator.CreateInstance(type);
        }

        public int Calculation()
        {
            int result;
            BaseOperate operate = CreateOperate(flag);// null;
            result = operate.Calculation(left, right);
            return result;
        }

        public int Calculation_back()
        {
            int result;
            BaseOperate operate = null;
            switch (flag)
            {

                case "+":
                    //  result = left + right;
                    operate = new Jia();
                    break;

                case "-":
                    //result = left - right;
                    operate = new Jian();
                    break;
                case "*":
                    //result = left * right;
                    operate = new Chen();
                    break;
                case "/":
                    //result = left / right;
                    operate = new Chu();
                    break;
            }
            result = operate.Calculation(left, right);
            return result;
        }
    }
}
