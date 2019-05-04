//==============================================================

//  作者：sun  (sunhlp@qq.com)

//  时间：1/9/2019 10:06:45 PM

//  文件名：AbstractAndInterface

//  版本：V1.0.1  

//  说明： 

//abstract:
//方法不能是私有的,必须是public
//方法不提供方法的实现
//继承abstract的子类，他只能继承一个抽象类，多个会报错 


//interface
//不能写public,无字段、常量，无构造函数
//实现interface的子类，可以实现多个接口
//  修改者：sun     
//  修改说明： 

//=================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class AbstractAndInterface
    {
    }

    public abstract class test1
    {
        public abstract void method1();
        public string name;

       
    }
    public abstract class test2
    {
        public string name { get; set; }
        public abstract void method2();

    }

    public interface sun1
    {

       
        void method1();



    }
    /// <summary>
    /// 继承abstract的子类，他只能继承一个抽象类，多个会报错 
    /// </summary>
    public class sontest : test1
    {
        public override void method1()
        {

            throw new NotImplementedException();
        }
    }

    public interface ISports
    {

        void Pingpan();
    }
    public interface IWork
    {
        void work();
    }

}
