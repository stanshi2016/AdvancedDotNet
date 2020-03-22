using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCLRCore
{
    /// <summary>
    /// 堆栈内存分配：什么内存？程序运行时，进程占有的内存, 谁来分配？CLR
    /// 值类型：struct(int), 枚举
    /// 引用类型：class,接口,委托
    /// 
    /// 线程栈：栈-stack,先进后出的数据结构,随着线程而分配的,默认执行方法分配1MB内存.
    /// 对象堆：内存，进程中独立画出来的一块儿内存，有一些对象不释放的，对象重用
    /// </summary>
    public class StackHeap
    {
        public static void Show()
        {
            //内存分配：线程栈,堆
            {
                //值类型分配在线程栈，变量和值都是在线程栈
                ValuePoint valuePoint;//先声明变量，没有初始化  但是我可以正常赋值  跟类不同
                valuePoint.x = 123;

                ValuePoint point = new ValuePoint();
                Console.WriteLine(valuePoint.x);
            }

            {   
                //引用类型分布在堆上面  变量是在栈上的，值是在堆上面
                //1 new的时候去堆开辟内存，分配一个地址
                //2 调用构造函数(因为在构造函数里可以使用this)，才执行构造函数
                //3 把引用传给变量
                ReferencePoint referencePoint = new ReferencePoint(123);
                Console.WriteLine(referencePoint.x);
            }
            {
                //// 引用类型嵌套值类型
                ReferenceTypeClass referenceTypeClassInstance = new ReferenceTypeClass();//Where is _valueTypeField?
                referenceTypeClassInstance.Method();//Where is valueTypeLocalVariable?
            }
            {

                ValueTypeStruct valueTypeStructInstance = new ValueTypeStruct();//Where is _referenceTypeField？
                valueTypeStructInstance.Method();//Where is referenceTypeLocalVariable?
            }
            //方法的局部变量：根据变量自身决定，跟所在的环境没关系
            //对象是引用类型，其属性/字段，都是在堆里面
            //对象是值类型，其属性/字段，值类型就在栈里  引用类型就在堆里

            //引用类型任何时候都在堆里；值类型都在栈里， 除非值类型所在对象是在堆里

            //装箱拆箱(仅仅是说内存的拷贝动作):内存copy 也会浪费性能  通常都是因为object，
            //装箱拆箱只能发生在父子类里面？ 因为这样你才能转换呀..
            {
                int i = 3;
                object oValue = i;
                //StackHeap stack = i;
                i = (int)oValue;
            }
            //引用类型在哪里   值类型在哪里？
            {
                ReferencePoint referencePoint = new ReferencePoint(3);
                Console.WriteLine(referencePoint.x);//3

                ReferencePoint referencePoint2 = referencePoint;
                Console.WriteLine(referencePoint2.x);//3

                referencePoint2.x = 123;
                Console.WriteLine(referencePoint.x);
                Console.WriteLine(referencePoint2.x);
            }
            {
                ValuePoint valuePoint = new ValuePoint();
                valuePoint.x = 3;
                Console.WriteLine(valuePoint.x);//3

                ValuePoint valuePoint2 = valuePoint;
                Console.WriteLine(valuePoint2.x);//3

                valuePoint2.x = 234;

                Console.WriteLine(valuePoint.x);
                Console.WriteLine(valuePoint2.x);
            }

            //string字符串内存分配
            {
                string student = "大山";//开辟一块儿内存  放下“大山”  返还一个引用（student变量）
                string student2 = student;//把student的引用copy一份儿给student2

                Console.WriteLine(student);
                Console.WriteLine(student2);

                student2 = "APP";
                //改了student2的值 但是不是修改内存；string字符串的内存是不可变的  
                //赋值其实是new string(APP),重新开辟内存，返回引用
                //不可变是因为享元，可能有多个变量指向同一个字符串，字符串变化了，多个变量都会受到影响
                //还因为堆里面的内存是连续分配的，如果变长度，会导致大量数据的移动
                Console.WriteLine(student);
                Console.WriteLine(student2);
            }

            {
                string student = "大山";
                string student2 = "APP";//共享
                student2 = "大山";
                Console.WriteLine(object.ReferenceEquals(student, student2));
                //就是同一个  享元模式 CLR内存分配字符串的时候，会查找相同值，有就重用了
                //

                //int[] intArray = new int[] { 123, 3232 };
                //int[] intArray2 = new int[3];
                //List<int> intList = new List<int>();//list性能就差一些
            }
            {
                StackTrace trace = new StackTrace();
                //获取是哪个类来调用的  
                Type type = trace.GetFrame(1).GetMethod().DeclaringType;
                //获取是类中的那个方法调用的  
                string method = trace.GetFrame(1).GetMethod().ToString();
            }
        }
    }


    public struct ValuePoint// : System.ValueType  结构不能有父类，因为隐式继承了ValueType
    {
        public int x;
        public ValuePoint(int x)
        {
            this.x = x;
        }
    }

    public class ReferencePoint
    {
        public int x;
        public ReferencePoint(int x)
        {
            this.x = x;
        }
    }

    /// <summary>
    /// class  引用类型
    /// </summary>
    public class ReferenceTypeClass
    {
        private int _valueTypeField;//堆：因为对象都在堆里，对象里面的属性也在堆里
        public ReferenceTypeClass()
        {
            _valueTypeField = 0;
        }
        public void Method()
        {
            int valueTypeLocalVariable = 0;//栈:全新的局部变量，线程栈来调用方法，然后分配内存
            new Process(); //堆: 引用类型.
        }
    }

    public struct ValueTypeStruct//值类型
    {
        private object _referenceTypeField;//1 堆1 栈2
        public ValueTypeStruct(int x)
        {
            _referenceTypeField = new object();
        }
        public void Method()
        {
            object referenceTypeLocalVariable = new object();//1 堆1栈2
        }
    }


}
