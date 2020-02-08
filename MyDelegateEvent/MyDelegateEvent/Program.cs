using MyDelegateEvent.DelegateExtend;
using MyDelegateEvent.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegateEvent
{
    /// <summary>
    /// 1 委托的声明、实例化和调用
    /// 2 泛型委托--Func Action
    /// 3 委托的意义：解耦 (减少重复代码)
    /// 4 委托的意义：异步多线程
    /// 5 委托的意义：多播委托
    /// 6 事件，观察者模式
    /// 
    /// 事件：可以把一堆可变的动作/行为封装出去，交给第三方来指定
    ///       预定义一样，程序设计的时候，我们可以把程序分成两部分
    ///       一部分是固定的，直接写死；还有不固定的地方，通过一个事件去开放接口，外部可以随意扩展动作
    /// 
    /// 框架：完成固定/通用部分，把可变部分留出扩展点，支持自定义
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    Console.WriteLine("委托的声明、实例化和调用 >>");
                    MyDelegateInit DelegateInit = new MyDelegateInit();
                    DelegateInit.Show();
                    Console.WriteLine("*********************************************");

                }
                {
                    Console.WriteLine("Delegate 解耦 >>");
                    DelegateDecoupling decoupling = new DelegateDecoupling();
                    decoupling.show();
                    Console.WriteLine("*********************************************");
                }
                {
                    Console.WriteLine("Delegate 多播委托 >>");
                    Cat cat = new Cat();
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Mouse().Run);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Baby().Cry);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Mother().Wispher);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Brother().Turn);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Father().Roar);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Neighbor().Awake);
                    cat.MiaoDelegateHandler += new MiaoDelegate(new Stealer().Hide);
                    cat.MiaoDelegateHandler += new Dog().Wang;
                    cat.MiaoNew();
                    Console.WriteLine("*********************************************");
                }

                {
                    //事件：是带event关键字的委托的实例，event可以限制变量被外部调用/直接赋值
                    //委托和事件的区别与联系？
                    //委托是c一个类型，比如Student
                    //事件是委托类型的一个实例

                    Cat cat = new Cat();
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Mouse().Run);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Baby().Cry);
                    // 事件无法调用Invoke
                    //cat.MiaoDelegateHandlerEvent.Invoke();
                    //cat.MiaoDelegateHandlerEvent = null;

                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Mother().Wispher);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Brother().Turn);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Father().Roar);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Neighbor().Awake);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Stealer().Hide);
                    cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Dog().Wang);
                    cat.MiaoNewEvent();
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
