using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace UnitTestProject1
{

    /// <summary>
    ///  MD5算法是单向散列算法的一种。单向散列算法也称为HASH算法，
    ///  是一种将任意长度的信息压缩至某一固定长度（称之为消息摘要）的函数(该压缩过程不可逆)
    /// </summary>
    [TestClass]
    public class MD5Test
    {
        [TestMethod]
        public void TestMethod1()
        { 
        }

        [TestMethod]
        public void FileToMD5()
        {
            //742kb -->29ms

            //string filepath = @"D:\1download\GenericInterface.rar";

            //106M--》1s
            //string filepath = @"D:\1download\EU-Installset-W3.9.0.0.zip";

            //3.28G-->37s
            // string filepath = @"D:\1soft\vs2015.ent_enu.iso";

            //文件名称不一样 但是内容一样，加密出来的值是一样的，可用来判断文件是否相同
            //string filepath = @"D:\1soft\测试md5加密文件 - 副本.txt";//3d63cca86c4233fd5f0dbec717dd1f11

            string filepath = @"D:\1soft\测试md5加密文件.txt";//3d63cca86c4233fd5f0dbec717dd1f11


            FileStream stream = new FileStream(filepath, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] value = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                sb.Append(value[i].ToString("x2"));
            }
            Console.WriteLine(sb.ToString());
        }

        [TestMethod]
        public void GetMD5Hash()
        {
            //输入的长度变化
            //输出的长度不变
            string input = "173fef9711b0cee030854a5b4dcc843f555";
            if (String.IsNullOrEmpty(input))
            {
                throw new Exception("Input can't be null.");
            }
            else
            {
                string output = string.Empty;
                MD5 md5Hasher = MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
                StringBuilder result = new StringBuilder();

                foreach (byte @byte in data)
                {
                    result.Append(@byte.ToString("x2"));
                }

                //32位的值输出
                Console.WriteLine(result.ToString());
            }
        }
    }
}
