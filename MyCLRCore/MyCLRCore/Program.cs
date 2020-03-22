using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCLRCore
{
    /// <summary>
    /// 1 .Net平台&CLR
    /// 2 堆栈内存分配：值类型和引用类型
    /// 3 垃圾回收和Dispose模式
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net高级班vip课程，今天是Eleven老师为大家带来的CLR核心机制");
                {
                    Console.WriteLine("******************StackHeap****************");
                    //StackHeap.Show();
                }
                {
                    Console.WriteLine("*****************GCDemo*****************");
                    GCDemo.Show();
                }
                {
                    Console.WriteLine("*****************StandardDispose*****************");
                    StandardDispose standardDispose = new StandardDispose();
                    standardDispose.Dispose();
                    standardDispose.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}

//（1）说栈是先进后出，可是我用值类型的时候，没感觉到是先进后出，我声明两个值类型，好像是随便用哪个都可以阿？
//先进 说的是声明变量的，内存的占用；后出，是释放内存的时候，后声明的先释放

//（3）GC回收就是IIS中进程池回收吗  不是的  进程池回收是网站停止，程序退出，当然，这个时候会发生一次GC

//（4）什么是非托管资源，什么是托管资源   堆里面的就是托管的，C#创建的就是对象
