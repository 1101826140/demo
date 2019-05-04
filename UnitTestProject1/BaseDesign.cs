using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{

    /// <summary>
    /// 工厂方法模式要求:尽量使用抽象类或接口来定义就可以达到一个开闭原则
    /// </summary>
    [TestClass]
    public class BaseDesign
    {
        [TestMethod]
        public void TestMethod1()
        {
            GetAnimalEat(new DogFactoryFactory());

            GetAnimalEat(new PigFactoryFactory());
        }

        void GetAnimalEat(AnimalFactory fa)
        {
            Animal am = fa.GetAnimal();
            am.Eat();
        }
        public abstract class Animal
        {
            public abstract void Eat();
        }

        public class Dog : Animal
        {
            public override void Eat()
            {
                Console.WriteLine("dog eat");
            }
        }

        public class Pig : Animal
        {
            public override void Eat()
            {
                Console.WriteLine("pig eat");
            }
        }

        public abstract class AnimalFactory
        {
            public abstract Animal GetAnimal();
        }

        public class DogFactoryFactory : AnimalFactory
        {
            public override Animal GetAnimal()
            {
                return new Dog();
            }

        }
        public class PigFactoryFactory : AnimalFactory
        {
            public override Animal GetAnimal()
            {
                return new Pig();
            }
        }
    }
}