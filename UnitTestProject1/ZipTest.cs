using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
 

namespace UnitTestProject1
{
    [TestClass]
    public class ZipTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //ZipHelper.UnZip(@"E:\ConsoleApplication\UnitTestProject1\resource\2019-02-28-16-36-09.zip", @"E:\ConsoleApplication\UnitTestProject1\resource\2019-02-28-16-36-09");
        }

        [TestMethod]
        public void Test2()
        {

            string zippath = @"E:\ConsoleApplication\UnitTestProject1\resource\2019-02-28-16-36-09.zip";


            string dirpath = @"E:\ConsoleApplication\UnitTestProject1\resource\2019-02-28-16-36-09-sun";


            // //打包
            System.IO.Compression.ZipFile.CreateFromDirectory
                 (dirpath, zippath.Replace("2019-02-28-16-36-09.zip", "123456.zip"),
              System.IO.Compression.CompressionLevel.Optimal, false);


            //当解压的文件夹已存在会报错
            //System.IO.Compression.ZipFile.ExtractToDirectory(zippath, dirpath); //解压
        }
    }
}
