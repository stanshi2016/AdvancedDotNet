using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegateEvent
{
    /// <summary>
    /// 委托：是一个类，继承自System.MulticastDelegate，里面内置了几个方法
    /// </summary>
    public delegate void NoReturnNoParaOutClass();
    public class MyDelegate
    {
        public delegate void NoReturnNoPara<T>(T t);
        public delegate void NoReturnNoPara();
        public delegate void NoReturnWithPara(int x, int y);//1 声明委托
        public delegate int WithReturnNoPara();
        public delegate string WithReturnWithPara(out int x, ref int y);
        public void Show()
        {
            //System.MulticastDelegate
            Student student = new Student()
            {
                Id = 123,
                Name = "靠谱一大叔",
                Age = 32,
                ClassId = 1
            };
            student.Study();
            {
                //把方法包装成对象，invoke的时候自动执行方法
                NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);//2 委托的实例化
                method.Invoke();//3 委托实例的调用
                method();

                this.DoNothing();
            }
            //begininvoke
            {
                WithReturnNoPara method = new WithReturnNoPara(this.GetSomething);
                int iResult = method.Invoke();
                iResult = method();
                var result = method.BeginInvoke(null, null);//异步调用
                method.EndInvoke(result);
            }
            {
                //多种途径实例化
                {
                    NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(DoNothingStatic);
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(Student.StudyAdvanced);
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(new Student().Study);
                }
            }

            { 
                //多播委托：一个变量保存多个方法，可以增减；invoke的时候可以按顺序执行
                //+= 为委托实例按顺序增加方法，形成方法链，Invoke时，按顺序依次执行
                Student studentNew = new Student();

                NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);
                method += new NoReturnNoPara(this.DoNothing);
                method += new NoReturnNoPara(DoNothingStatic);
                method += new NoReturnNoPara(Student.StudyAdvanced);
                method += new NoReturnNoPara(new Student().Study);//不是同一个实例，所以是不同的方法
                method += new NoReturnNoPara(studentNew.Study);
                method.Invoke();

                //method.BeginInvoke(null, null);//多播委托是不能异步的
                foreach (NoReturnNoPara item in method.GetInvocationList())
                {
                    item.Invoke();
                    //item.BeginInvoke(null, null);
                }
                //-= 为委托实例移除方法，从方法链的尾部开始匹配，遇到第一个完全吻合的，移除且只移除一个，没有也不异常
                method -= new NoReturnNoPara(this.DoNothing);
                method -= new NoReturnNoPara(DoNothingStatic);
                method -= new NoReturnNoPara(Student.StudyAdvanced);
                method -= new NoReturnNoPara(new Student().Study);
                method -= new NoReturnNoPara(studentNew.Study);
                method.Invoke();
            }
            {
                WithReturnNoPara method = new WithReturnNoPara(this.GetSomething);
                method += new WithReturnNoPara(this.GetSomething2);
                method += new WithReturnNoPara(this.GetSomething3);
                int iResult = method.Invoke();//多播委托带返回值，结果以最后的为准
            }
        }



        private int GetSomething()
        {
            return 1;
        }
        private int GetSomething2()
        {
            return 2;
        }

        private int GetSomething3()
        {
            return 3;
        }
        private void DoNothing()
        {
            Console.WriteLine("This is DoNothing");
        }
        private static void DoNothingStatic()
        {
            Console.WriteLine("This is DoNothingStatic");
        }
    }

    public class OtherClass
    {
        public void DoNothing()
        {
            Console.WriteLine("This is DoNothing");
        }
        public static void DoNothingStatic()
        {
            Console.WriteLine("This is DoNothingStatic");
        }
    }
}
