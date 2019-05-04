//==============================================================

//  作者：sun  (sunhlp@qq.com)

//  时间：1/9/2019 10:19:57 PM

//  文件名：DelegateAndEvent

//  版本：V1.0.1  

//  说明： 
// 委托可以解除公用逻辑，减少重复代码，解除公用逻辑的和业务逻辑的耦合

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
    public class DelegateAndEvent
    {


    }

    public class StudentUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public int Age { get; set; }

        public static void Show()
        {
            Console.WriteLine("123");
        }
        private List<StudentUser> GetStudentUserList()
        {
            #region 初始化数据
            List<StudentUser> StudentUserList = new List<StudentUser>()
            {
                new StudentUser()
                {
                    Id=1,
                    Name="老K",
                    ClassId=2,
                    Age=35
                },
                new StudentUser()
                {
                    Id=1,
                    Name="hao",
                    ClassId=2,
                    Age=23
                },
                 new StudentUser()
                {
                    Id=1,
                    Name="大水",
                    ClassId=2,
                    Age=27
                },
                 new StudentUser()
                {
                    Id=1,
                    Name="半醉人间",
                    ClassId=2,
                    Age=26
                },
                new StudentUser()
                {
                    Id=1,
                    Name="风尘浪子",
                    ClassId=2,
                    Age=25
                },
                new StudentUser()
                {
                    Id=1,
                    Name="一大锅鱼",
                    ClassId=2,
                    Age=24
                },
                new StudentUser()
                {
                    Id=1,
                    Name="小白",
                    ClassId=2,
                    Age=21
                },
                 new StudentUser()
                {
                    Id=1,
                    Name="yoyo",
                    ClassId=2,
                    Age=22
                },
                 new StudentUser()
                {
                    Id=1,
                    Name="冰亮",
                    ClassId=2,
                    Age=34
                },
                 new StudentUser()
                {
                    Id=1,
                    Name="瀚",
                    ClassId=2,
                    Age=30
                },
                new StudentUser()
                {
                    Id=1,
                    Name="毕帆",
                    ClassId=2,
                    Age=30
                },
                new StudentUser()
                {
                    Id=1,
                    Name="一点半",
                    ClassId=2,
                    Age=30
                },
                new StudentUser()
                {
                    Id=1,
                    Name="小石头",
                    ClassId=2,
                    Age=28
                },
                new StudentUser()
                {
                    Id=1,
                    Name="大海",
                    ClassId=2,
                    Age=30
                },
                 new StudentUser()
                {
                    Id=3,
                    Name="yoyo",
                    ClassId=3,
                    Age=30
                },
                  new StudentUser()
                {
                    Id=4,
                    Name="unknown",
                    ClassId=4,
                    Age=30
                }
            };
            #endregion
            return StudentUserList;
        }


        /// <summary>
        /// 条件一：年纪超过25岁的
        /// </summary>
        /// <returns></returns>
        public List<StudentUser> getAgeThan25()
        {

            var list = this.GetStudentUserList();
            List<StudentUser> result = new List<StudentUser>();
            foreach (StudentUser su in list)
            {

                if (su.Age > 25)
                {
                    result.Add(su);
                }
            }
            return result;
        }



        public List<StudentUser> getClassId2()
        {

            var list = this.GetStudentUserList();
            List<StudentUser> result = new List<StudentUser>();
            foreach (StudentUser su in list)
            {

                if (su.Age > 25)
                {
                    result.Add(su);
                }
            }
            return result;
        }

        public delegate bool ConditionDelegate(StudentUser stu);

        public bool Condition1(StudentUser su)
        {

            return su.Age > 25;
        }

        public bool Condition2(StudentUser su)
        {

            return su.ClassId==2;
        }
        public List<StudentUser> GetDataByCondition(List<StudentUser> source, ConditionDelegate mehtod)
        {
            var list = source;
            List<StudentUser> result = new List<StudentUser>();
            foreach (StudentUser su in list)
            {
                //调用委托 执行

                if (mehtod.Invoke(su))
                {
                    result.Add(su);
                }
            }
            return result;
        }
        public delegate void NotReturnData();
        public delegate void NotReturnParas<T>(T t);
        private void DoNothing()
        {
            Console.WriteLine("This is DoNothing");
        }

        public static void DoNothing2() { }

        public static void DoNothingStatic() { }
        public void OutPut()
        {

            //调用有参数有返回值的写法
            ConditionDelegate method = new ConditionDelegate(Condition1);
            List<StudentUser> result = GetDataByCondition(this.GetStudentUserList(), method);

            ConditionDelegate method2 = new ConditionDelegate(Condition2);
            List<StudentUser> result2 = GetDataByCondition(this.GetStudentUserList(), method2);


            //调用无参无返回值的写法
            NotReturnData method3 = new NotReturnData(DoNothing);
            method3.Invoke();
            method3();
            this.DoNothing();
            method3 += new NotReturnData(DoNothing2);
        }
    }

}
