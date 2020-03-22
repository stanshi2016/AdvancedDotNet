using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCLRCore
{
    /// <summary>
    /// 垃圾回收
    /// </summary>
    public class GCDemo
    {
        private static Student _Student = new Student()//静态的不可能被回收   静态持有的引用也不会被回收
        {
            Id = 123,
            Name = "Eleven"
        };
        public static void Show()
        {

            //值类型出现在线程栈：每次调用都有线程栈，，用完自己就结束，变量 - 值类型 都会释放的
            //引用类型出现在堆里：全局就一个堆，空间有限，所以才需要垃圾回收
            //操作系统里面，内存是链式分配的，可能有碎片的
            //CLR的堆：连续分配(数组)，空间有限，节约空间

            //gc程序退出的时候也会gc
            //gc发生在new的时候  new一个对象时，会开辟内存，看看空间够不够，不够的话就要GC了
            //定时程序，24小时执行一次，但是对象不会被回收，因为24小时之后你才会new 这个才可能发生GC 所以要手动GC

            //怎么回收？ 
            //什么是垃圾？ 垃圾是完全访问不到的东西了，
            //new的时候发现内存不够  就去遍历所有堆的对象，标记访问不到，然后启动一个线程来清理内存
            //清除标记了的对象，其他挪动，然后整齐摆放，所以这个时候全部线程停止，不允许操作内存

            //内存不够的是指一级对象的内存，有个临界值，也不是全部的堆的大小

            //但是  ~Student() 析构函数 类型名称价加个~  单独的处理，把这些对象放入一个队列单独处理，但是不知道什么时候去调用后析构函数
            //主要是用来释放非托管资源

            //2个优化策略   分级策略：
            //1 首次GC前 全部对象都是0级  
            //2 第一次GC后，还保留的对象叫1级
            //3 回收先找0级对象，如果空间还不够，再去找1级对象，这之后，还存在的对象就变成2级
            //4 0级不够，1级也不够，2级还不够，那就内存溢出了
            //越是最近分配的，越是会被回收   比如for循环创建对象

            //内存泄露：内存占用了，没有回收；静态属性引用，

            //大对象策略：如果大于某个值的对象85k，单独管理，用的是链表(碎片)，避免频繁的内存移动

          
            {
                Student student = _Student;
                Class @class = new Class()
                {
                    ClassId = 1,
                    ClassName = "高级班"
                };
                student.Class = @class;
                int i = 3;//都会被GC
                GCDemo gCDemo = new GCDemo();//都会被GC
            }
            {
                GC.Collect();//主动GC
            }
            {
                Student student = new Student()
                {
                    Id = 234,
                    Name = "Eleven",
                    Class = new Class()
                    {
                        ClassId = 1,
                        ClassName = "高级班"
                    }
                };
                student = null;//编译器会无视这句话，不是改成null这个对象没有了，其实内存还占用着
                //为了促进垃圾回收，把对象赋值成null  其实不对的，没有意义，垃圾回收是因为访问不到
                //静态可以，这样可以让变量不再被引用
                GC.Collect();
            }
            {
                //~Student() 析构函数  主要是用来释放非托管资源，等着GC的时候去把非托管资源释放掉  系统自动执行
                //GC回收的时候，CLR一定调用的，但是可能有延迟(释放对象不知道要多久呢)

                //Dispose() 也是释放非托管资源的，主动释放，方法本身是没有意义的，我们需要在方法里面实现对资源的释放 
                //GC不会调用，而是用对象时，使用者主动调用这个方法，去释放非托管资源

                //而不是说对象释放的时候，会去自动调用Dispose方法
                //然后在用完对象时，我们主动去执行dispose方法，当然可以是using的快捷方式
                using (Student student = new Student()
                {
                    Id = 234,
                    Name = "Eleven",
                    Class = new Class()
                    {
                        ClassId = 1,
                        ClassName = "高级班"
                    }
                })
                {
                    Console.WriteLine("这里啥也不干");
                }

                try
                { }
                finally
                {
                    //调用的dispose()
                }

            }
            {
                for (int i = 0; i < 1000; i++)
                {
                    Class @class = new Class()
                    {
                        ClassId = i,
                        ClassName = "高级班"
                    };
                }
                GC.Collect();
            }
        }
    }

    public class People : IDisposable
    {
        public string Remark { get; set; }
        public virtual void Dispose()
        {
            MyLog.Log($"执行{this.GetType().Name}Dispose");
        }
    }

    public class Student : People, IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Class Class { get; set; }

        public override void Dispose()//提供主动释放方式
        {
            base.Dispose();//把我引用的其他东西给清理掉
            if (this.Class != null)
            {
                this.Class.Dispose();
            }
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
            MyLog.Log($"执行{this.GetType().Name}Dispose");
        }

        public void Study()
        {
            Console.WriteLine("跟着Eleven老师学习.Net高级开发");
        }

        ~Student()//保证垃圾回收时  一定会把非托管资源释放
        {
            MyLog.Log($"执行{this.GetType().Name}Dispose");
        }
    }

    public class Class : IDisposable
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        //~Class()
        //{
        //    MyLog.Log($"执行{this.GetType().Name}Dispose");
        //}
        public void Dispose()
        {
            MyLog.Log($"执行{this.GetType().Name}Dispose");
        }
    }


}
