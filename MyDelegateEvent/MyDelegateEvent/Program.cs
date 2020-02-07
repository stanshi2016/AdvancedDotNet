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
    /// 明天软谋前端高级班开班典礼，我需要出席一下；
    /// 软谋的微信小程序再度起航，明天晚上八点是第一堂体验课；(欢迎来听下)
    /// 所以明天的课程推迟；
    /// 本周五六日软谋公司旅游，去庐山会议；
    /// 
    /// 1 委托的声明、实例化和调用
    /// 2 泛型委托--Func Action
    /// 3 委托的意义：解耦
    /// 4 委托的意义：异步多线程
    /// 5 委托的意义：多播委托
    /// 6 事件，观察者模式
    /// 
    /// 小作业：数据库增删改查的时候，试试委托封装
    /// 
    /// 委托解耦，减少重复代码
    /// 异步多线程
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
                Console.WriteLine("欢迎来到.net高级班VIP课程，今天是Eleven老师为大家带来的委托事件的学习");
                {
                    //Console.WriteLine("****************************MyDelegate*************************");
                    //MyDelegate myDelegate = new MyDelegate();
                    //myDelegate.Show();
                }
                {
                    //ListExtend test = new ListExtend();
                    //test.Show();
                }
                {
                    Console.WriteLine("****************************Event*************************");

                    {
                        Cat cat = new Cat();
                        cat.Miao();
                        Console.WriteLine("***************************");
                    }
                    {
                        Cat cat = new Cat();
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Mouse().Run);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Baby().Cry);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Mother().Wispher);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Brother().Turn);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Father().Roar);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Neighbor().Awake);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Stealer().Hide);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Dog().Wang);
                        cat.MiaoNew();
                        Console.WriteLine("***************************");
                    }
                    {
                        Cat cat = new Cat();
                        //cat.MiaoDelegateHandler += new MiaoDelegate(new Mouse().Run);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Baby().Cry);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Mother().Wispher);

                        //cat.MiaoDelegateHandler.Invoke();
                        //cat.MiaoDelegateHandler = null;

                        cat.MiaoDelegateHandler += new MiaoDelegate(new Brother().Turn);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Father().Roar);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Neighbor().Awake);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Stealer().Hide);
                        cat.MiaoDelegateHandler += new MiaoDelegate(new Dog().Wang);
                        cat.MiaoNew();
                        Console.WriteLine("***************************");
                    }


                    {
                        Cat cat = new Cat();
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Mouse().Run);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Baby().Cry);
                        //cat.MiaoDelegateHandlerEvent.Invoke();
                        //cat.MiaoDelegateHandlerEvent = null;

                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Mother().Wispher);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Brother().Turn);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Father().Roar);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Neighbor().Awake);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Stealer().Hide);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Dog().Wang);
                        cat.MiaoNewEvent();
                        Console.WriteLine("***************************");
                    }
                    {
                        Cat cat = new Cat();
                        //cat.MiaoDelegateHandler += new MiaoDelegate(new Mouse().Run);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Baby().Cry);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Mother().Wispher);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Brother().Turn);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Father().Roar);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Neighbor().Awake);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Stealer().Hide);
                        cat.MiaoDelegateHandlerEvent += new MiaoDelegate(new Dog().Wang);
                        cat.MiaoNewEvent();
                        Console.WriteLine("***************************");
                    }
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
