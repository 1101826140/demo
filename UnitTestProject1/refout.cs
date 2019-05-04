using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class refout
    {
        [TestMethod]
        public void TestMethod1()
        {

            string name;
            GetName(out name);

            int age = 0;
            GetAge(ref age);
        }


        public void GetName(out string name)
        {
            name = "sun";
        }

        public void GetAge(ref int age)
        {
            age = 26;
        }

    }
}
