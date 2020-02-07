using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegateEvent.DelegateExtend
{
    public class ListExtend
    {
        #region 数据准备
        /// <summary>
        /// 准备数据
        /// </summary>
        /// <returns></returns>
        private List<Student> GetStudentList()
        {
            #region 初始化数据
            List<Student> studentList = new List<Student>()
            {
                new Student()
                {
                    Id=1,
                    Name="老K",
                    ClassId=2,
                    Age=35
                },
                new Student()
                {
                    Id=1,
                    Name="hao",
                    ClassId=2,
                    Age=23
                },
                 new Student()
                {
                    Id=1,
                    Name="大水",
                    ClassId=2,
                    Age=27
                },
                 new Student()
                {
                    Id=1,
                    Name="半醉人间",
                    ClassId=2,
                    Age=26
                },
                new Student()
                {
                    Id=1,
                    Name="风尘浪子",
                    ClassId=2,
                    Age=25
                },
                new Student()
                {
                    Id=1,
                    Name="一大锅鱼",
                    ClassId=2,
                    Age=24
                },
                new Student()
                {
                    Id=1,
                    Name="小白",
                    ClassId=2,
                    Age=21
                },
                 new Student()
                {
                    Id=1,
                    Name="yoyo",
                    ClassId=2,
                    Age=22
                },
                 new Student()
                {
                    Id=1,
                    Name="冰亮",
                    ClassId=2,
                    Age=34
                },
                 new Student()
                {
                    Id=1,
                    Name="瀚",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="毕帆",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="一点半",
                    ClassId=2,
                    Age=30
                },
                new Student()
                {
                    Id=1,
                    Name="小石头",
                    ClassId=2,
                    Age=28
                },
                new Student()
                {
                    Id=1,
                    Name="大海",
                    ClassId=2,
                    Age=30
                },
                 new Student()
                {
                    Id=3,
                    Name="yoyo",
                    ClassId=3,
                    Age=30
                },
                  new Student()
                {
                    Id=4,
                    Name="unknown",
                    ClassId=4,
                    Age=30
                }
            };
            #endregion
            return studentList;
        }
        #endregion


        public delegate bool ThanDelegate(Student student);
        private bool Than(Student student)
        {
            return student.Age > 25;
        }
        private bool LengthThan(Student student)
        {
            return student.Name.Length > 2;
        }
        private bool AllThan(Student student)
        {
            return student.Name.Length > 2 && student.Age > 25 && student.ClassId == 2;
        }

        public void Show()
        {
            List<Student> studentList = this.GetStudentList();
            {
                //找出年龄大于25
                List<Student> result = new List<Student>();//准备容器
                foreach (Student student in studentList)//遍历数据源
                {
                    if (student.Age > 25)//判断条件
                    {
                        result.Add(student);//满足条件的放入容器
                    }
                }
                Console.WriteLine($"结果一共有{result.Count()}个");
            }
            {
                //this.GetList(studentList, 1);
                ThanDelegate method = new ThanDelegate(this.Than);
                List<Student> result = this.GetListDelegate(studentList, method);
                Console.WriteLine($"结果一共有{result.Count()}个");
            }

            {
                //找出Name长度大于2
                List<Student> result = new List<Student>();
                foreach (Student student in studentList)
                {
                    if (student.Name.Length > 2)
                    {
                        result.Add(student);
                    }
                }
                Console.WriteLine($"结果一共有{result.Count()}个");
            }
            {
                //this.GetList(studentList, 2);
                ThanDelegate method = new ThanDelegate(this.LengthThan);
                List<Student> result = this.GetListDelegate(studentList, method);
                Console.WriteLine($"结果一共有{result.Count()}个");
            }
            {
                //找出Name长度大于2 而且年龄大于25 而且班级id是2
                List<Student> result = new List<Student>();
                foreach (Student student in studentList)
                {
                    if (student.Name.Length > 2 && student.Age > 25 && student.ClassId == 2)
                    {
                        result.Add(student);
                    }
                }
                Console.WriteLine($"结果一共有{result.Count()}个");
            }
            {
                //this.GetList(studentList, 3);
                ThanDelegate method = new ThanDelegate(this.AllThan);
                List<Student> result = this.GetListDelegate(studentList, method);
                Console.WriteLine($"结果一共有{result.Count()}个");
            }
        }

        /// <summary>
        /// 1 type:不同的值 不同的判断  如果再多一种
        /// 
        /// 传个参数--判断参数--执行对应的行为
        /// 那我能不能直接传递个行为呢？逻辑就是方法，等于说传递方法进来
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private List<Student> GetList(List<Student> source, int type)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in source)
            {
                if (type == 1)
                {
                    if (student.Age > 25)//判断条件
                    {
                        result.Add(student);//满足条件的放入容器
                    }
                }
                else if (type == 2)
                {
                    if (student.Name.Length > 2)
                    {
                        result.Add(student);
                    }
                }
                else if (type == 3)
                {
                    if (student.Name.Length > 2 && student.Age > 25 && student.ClassId == 2)
                    {
                        result.Add(student);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 判断逻辑传递进来+实现共用逻辑
        /// 委托解耦，减少重复代码
        /// </summary>
        /// <param name="source"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private List<Student> GetListDelegate(List<Student> source, ThanDelegate method)
        {
            List<Student> result = new List<Student>();
            foreach (Student student in source)
            {
                if (method.Invoke(student))
                {
                    result.Add(student);
                }
            }
            return result;
        }
    }
}
