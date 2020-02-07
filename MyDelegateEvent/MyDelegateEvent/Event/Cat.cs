using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegateEvent.Event
{
    /// <summary>
    /// 发布者
    /// 一只猫，miao一声
    /// 导致一系列的触发动作
    /// 
    /// 直接调用别的实例的别的方法
    /// 不管是增/减/顺序  都会让猫不稳定
    /// 
    /// 发布者
    /// </summary>
    public class Cat
    {
        public void Miao()
        {
            Console.WriteLine("{0} Miao", this.GetType().Name);

            new Mouse().Run();
            new Baby().Cry();
            new Mother().Wispher();
            //new Brother().Turn();
            new Father().Roar();
            new Neighbor().Awake();
            new Stealer().Hide();
            new Dog().Wang();
        }

        //猫 叫一声   触发一系列后续动作  
        //多了个 指定动作  正是这个不稳定   封装出去   甩锅
        public MiaoDelegate MiaoDelegateHandler;
        public void MiaoNew()
        {
            Console.WriteLine("{0} MiaoNew", this.GetType().Name);
            if (this.MiaoDelegateHandler != null)
            {
                this.MiaoDelegateHandler.Invoke();
            }
        }

        //事件：是带event关键字的委托的实例，event可以限制变量被外部调用/直接赋值
        //委托和事件的区别与联系？
        //委托是一个类型，比如Student
        //事件是委托类型的一个实例，比如 果然
        public event MiaoDelegate MiaoDelegateHandlerEvent;
        public void MiaoNewEvent()
        {
            Console.WriteLine("{0} MiaoNewEvent", this.GetType().Name);
            if (this.MiaoDelegateHandlerEvent != null)
            {
                this.MiaoDelegateHandlerEvent.Invoke();
            }
        }

        //观察者模式

    }

    public class ChildClass : Cat
    {
        public void Show()
        {
            this.MiaoDelegateHandlerEvent += null;
            //if (this.MiaoDelegateHandlerEvent != null)//子类也不能调用
            //{
            //    this.MiaoDelegateHandlerEvent.Invoke();
            //}
        }

    }

    public delegate void MiaoDelegate();
}
