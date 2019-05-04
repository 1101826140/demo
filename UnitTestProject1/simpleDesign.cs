using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{

    /// <summary>
    /// 简单工厂模式的缺点：
    ///增加具体产品时，需要修改工厂类里面生成具体产品的方法，这就违反了开闭原则。具体产品经常发生变化时，不建议使用简单工厂模式。
    /// 
    /// </summary>
    [TestClass]
    public class simpleDesign
    {
        [TestMethod]
        public void TestMethod1()
        {
            IPeople people = new PeopleFactory().NewInstance(1);
            people.SayHello();
        }
    }

    public interface IPeople
    {
        void SayHello();
    }

    public class ChinesePeople : IPeople
    {
        public void SayHello()
        {
            Console.WriteLine("早上好，吃了吗");
        }
    }

    public class EnglandPeople : IPeople
    {
        public void SayHello()
        {
            Console.WriteLine("hello?");
        }
    }

    public class PeopleFactory
    {

        public IPeople NewInstance(int witch)
        {
            IPeople people = null;
            switch (witch)
            {
                case 1:
                    people = new ChinesePeople();
                    break;
                case 2:
                    people = new EnglandPeople();
                    break;
            }
            return people;
        }

    }
}
