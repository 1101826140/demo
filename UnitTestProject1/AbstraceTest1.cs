using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class AbstraceTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AbstraceAnimal an = new Dog();
            an.Shout();
        }
    }

    public class BaseAnimal
    {
        public BaseAnimal()
        {
            Console.WriteLine("animal");
        }
    }

    public abstract  class AbstraceAnimal
    {
        public abstract void Shout();
    }

    public class Dog: AbstraceAnimal
    {
        public Dog()
        {
            Console.WriteLine("dog");
        }

        public override void Shout()
        {
            Console.WriteLine("shout");
        }
    }
}
