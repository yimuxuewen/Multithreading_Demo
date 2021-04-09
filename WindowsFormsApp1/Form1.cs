using Sync_Async_Demo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 同步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************同步方法开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Action<string> action = this.DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                action.Invoke($"BtnSync_{i}");
            }
            Console.WriteLine($"***************同步方法结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();

        }

        /// <summary>
        /// 异步调用方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAsync_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************异步方法开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Action<string> action = this.DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                action.BeginInvoke($"BtnSync_{i}", null, null);
            }

            Console.WriteLine($"***************异步方法结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();
        }

        /// <summary>
        /// 耗时操作
        /// </summary>
        /// <param name="name"></param>
        private void DoSomethingLong(string name)
        {
            Console.WriteLine($"***************DoSomething start {name} {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss:fffff")} ***************");
            long result = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                result += i;
            }
            Console.WriteLine($"***************DoSomething end {name} {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss:fffff")} {result}***************");

        }

        /// <summary>
        /// 返回计算值
        /// </summary>
        /// <returns></returns>
        private long RemoteService()
        {
            Console.WriteLine($"***************RemoteService start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss:fffff")} ***************");
            long result = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                result += i;
            }
            Console.WriteLine($"***************RemoteService end {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss:fffff")} ***************");
            return result;
        }

        private void BtnAsyncAdvanced_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************异步方法BtnAsyncAdvanced开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Action<string> action = this.DoSomethingLong;
            #region 异步回调
            //AsyncCallback asyncCallback = ay =>
            //{
            //    Console.WriteLine(ay.AsyncState);
            //    Console.WriteLine($"***************BtnAsyncAdvanced_Click操作已完成 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            //};
            //for (int i = 0; i < 5; i++)
            //{
            //    action.BeginInvoke($"BtnSync_{i}", asyncCallback, "run stop");
            //}

            #endregion

            #region 异步等待
            //IAsyncResult asyncResult = action.BeginInvoke("异步等待，误差200ms", null, null);
            //int i = 0;
            //while (!asyncResult.IsCompleted)
            //{
            //    if (i < 9)
            //    {
            //        Console.WriteLine($"完成进度为{++i * 10}%");
            //    }
            //    else
            //    {
            //        Console.WriteLine("完成进度为99.999%");
            //    }
            //    Thread.Sleep(200);
            //}
            //Console.WriteLine("操作完成");

            #endregion

            #region 信号量，可不需等待
            IAsyncResult asyncResult = action.BeginInvoke("根据信号量判断异步是否完成", null, null);
            Console.WriteLine("Do Other Things");
            Console.WriteLine("Do Other Things");
            Console.WriteLine("Do Other Things");
            //asyncResult.AsyncWaitHandle.WaitOne();//代码异步操作完成，无延时
            //asyncResult.AsyncWaitHandle.WaitOne(-1);//一直等待，直到异步任务完成
            asyncResult.AsyncWaitHandle.WaitOne(1000);//最多等待1000ms，超过这个时间，异步任务未完成时，直接执行后面操作

            #endregion
            Console.WriteLine($"***************异步方法BtnAsyncAdvanced结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();

        }

        private void BtnGetResult_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************异步方法BtnGetResult开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Func<long> func = this.RemoteService;
            IAsyncResult result = func.BeginInvoke(null, null);
            long returnvalue = func.EndInvoke(result);//调用EndInvoke获取异步返回值
            Console.WriteLine($"异步返回值为：{returnvalue}");
            Console.WriteLine($"***************异步方法BtnGetResult结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();

        }

        /// <summary>
        /// 不同版本多线程使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMutiple_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************异步方法BtnMutiple开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            //{
            //    //.Net Framework1.0 1.1
            //    ThreadStart threadStart = () =>
            //    {
            //        Console.WriteLine($"Thread Demo Start {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"Thread Demo End {Thread.CurrentThread.ManagedThreadId}");
            //    };
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //    //thread.Join();
            //    //thread.Abort();
            //    //thread.Resume();
            //    //thread.IsBackground();
            //    //thread.Suspend();
            //    //Thread提供的方法丰富，可以玩的很花哨，但都玩不好--因为线程资源是操作系统管理的，响应并不及时，没那么好控制
            //    //Thread线程启动过多时，会导致电脑死机
            //}

            //{
            //    //.Net Framework 2.0
            //    //ThreadPool 池化资源管理设计思想，线程是一种资源，之前每次要用线程，就去申请一个线程，使用之后，释放线程；池化就是一个容器，容器提前申请线程，程序要使用线程时，直接找容器获取，用完之后再放回容器（控制状态），避免频繁的申请和销毁；容器自己可根据限制的数量去申请和释放
            //    WaitCallback waitCallback = o =>
            //    {
            //        Console.WriteLine($"ThreadPool Demo Start {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"ThreadPool Demo End {Thread.CurrentThread.ManagedThreadId}");
            //    };
            //    ThreadPool.QueueUserWorkItem(waitCallback);
            //    //ThreadPool.GetAvailableThreads();
            //    //ThreadPool.GetMaxThreads();
            //    //ThreadPool.GetMinThreads(out int workerthreads, out int completionPortthreads);
            //    //ThreadPool.SetMinThreads();
            //    //ThreadPool.SetMaxThreads();
            //    //ThreadPool API 较少，线程等待控制顺序较弱，影响应用
            //}

            {
                //.Net Framework 3.0 Task 多线程的最佳实战
                //Task线程全部是线程池中的线程
                //提供了丰富的API，非常适合开发实现
                //Action action = () =>
                //{
                //    Console.WriteLine($"Task Demo Start {Thread.CurrentThread.ManagedThreadId}");
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"Task Demo End {Thread.CurrentThread.ManagedThreadId}");
                //};
                //Task task = new Task(action);
                //task.Start();
            }

            //{
            //    //Parallel可以启动多线程，主线程也参与计算，节约一个线程
            //    //可以通过ParallelOptions控制最大并发数量
            //    Parallel.Invoke(() =>
            //    {
            //        Console.WriteLine($"Parallel Demo Start1 {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"Parallel Demo End1 {Thread.CurrentThread.ManagedThreadId}");
            //    },
            //    () =>
            //    {
            //        Console.WriteLine($"Parallel Demo Start2 {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"Parallel Demo End2 {Thread.CurrentThread.ManagedThreadId}");
            //    }, () =>
            //    {
            //        Console.WriteLine($"Parallel Demo Start3 {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"Parallel Demo End3 {Thread.CurrentThread.ManagedThreadId}");
            //    }
            //    , () =>
            //    {
            //        Console.WriteLine($"Parallel Demo Start4 {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"Parallel Demo End4 {Thread.CurrentThread.ManagedThreadId}");
            //    });
            //}

            {

            }
            Console.WriteLine($"***************异步方法BtnMutiple结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTaskSafe_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"***************异步方法BtnTaskSafe开始 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");

            #region 多线程下变量作用域
            ////i 全部是5  --声明i==0，然后循环第1次，此时动作没有启动 ---声明i==1，然后循环第1次，此时动作没有启动 ---直到i==5时，线程启动，所有都是5
            //for (int i = 0; i < 5; i++)
            //{
            //    //在作用域呢，每个k在内存分配不同，0,1,2,3,4
            //    int k = i;
            //    Task.Run(()=> {
            //        Console.WriteLine($"This is {i} {k} run start {Thread.CurrentThread.ManagedThreadId}");
            //        Thread.Sleep(2000);
            //        Console.WriteLine($"This is {i} {k} run end {Thread.CurrentThread.ManagedThreadId}");
            //    });
            //}

            #endregion

            #region 多线程下修改集合

            ////多线程去访问同一个集合，有问题么？一般是没有问题，线程安全是出现在修改同一个对象时出现的
            //List<int> list = new List<int>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        list.Add(i);
            //    });
            //}
            //Thread.Sleep(5000);
            //Console.WriteLine($"list集合的数量为{list.Count}");
            ////多线程安全问题：一段代码，单线程执行和多线程执行结果不一样，就表明有线程安全问题
            ////list是个数据结构，在内存上是连续摆放的，假如在同一时刻，去增加一个数据，都是操作同一个内存位置，2个cpu同时发出了指令，内存先执行一个再执行一个，就会出现覆盖
            #endregion

            #region 解决多线程安全问题
            //List<int> list = new List<int>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    Task.Run(() =>
            //    {
            //        Monitor.Enter(locks);
            //        lock (locks)
            //        {
            //            list.Add(i);
            //        }
            //        Monitor.Exit(locks);
            //    });
            //}
            //Thread.Sleep(5000);
            //Console.WriteLine($"list集合的数量为{list.Count}");
            ////加lock解决线程安全问题 ---单线程化 ----lock加锁，保证在任意时刻，只有一个线程可操作对象，即单线程化
            ////lock原理---语法糖 ---等同于 Monitor ---锁定一个内存引用地址，所以不能是值类型，也不能是null
            #endregion

            #region lock测试
            {
                //TestLock.Show();

                //for (int i = 0; i < 5; i++)
                //{
                //    int k = i;
                //    Task.Run(() =>
                //    {
                //        //lock (TestLock.locks) //同用一个lock锁，无法并发执行
                //        lock (locks)
                //        {
                //            Console.WriteLine($"This is Main {i} {k} run start {Thread.CurrentThread.ManagedThreadId}");
                //            Thread.Sleep(2000);
                //            Console.WriteLine($"This is Main {i} {k} run end {Thread.CurrentThread.ManagedThreadId}");
                //        }
                //    });
                //}
                ////假如我们希望主程序和方法并发执行，如果共用一个变量锁，就会出现相互阻塞
                ////锁不同变量，才能并发
            }

            {
                //TestLock testLock1 = new TestLock();
                //testLock1.ShowTmp(1);
                //TestLock testLock2 = new TestLock();
                //testLock1.ShowTmp(2);
                ////单次执行内部是加锁运行的，两个方法之间执行是并发的，lock锁定的是不同的引用地址
            }

            {
                //TestLock testLock1 = new TestLock();
                //testLock1.ShowString();
                //GCHandle handle = GCHandle.Alloc(lockstring);
                //var pin = GCHandle.ToIntPtr(handle);
                //Console.WriteLine(pin);

                //for (int i = 0; i < 5; i++)
                //{
                //    int k = i;
                //    Task.Run(() =>
                //    {
                //        lock (lockstring)
                //        {
                //            Console.WriteLine($"This is Main {i} {k} run start {Thread.CurrentThread.ManagedThreadId}");
                //            Thread.Sleep(2000);
                //            Console.WriteLine($"This is Main {i} {k} run end {Thread.CurrentThread.ManagedThreadId}");
                //        }
                //    });
                //}
                ////单次执行内部是加锁运行的，理论上主线程和方法间不能并发执行，字符串是享元的，lock锁定的是同一引用地址 --但实际是可并发的，锁定的引用地址并非是同一个
            }

            {
                TestLockGeneric<int>.Show(1);
                TestLockGeneric<int>.Show(2);
                TestLockGeneric<string>.Show(3);

                //1和2不可并发运行 1和3可并发运行
                //1和2不能并发，因为是相同的变量--泛型类，在变量参数相同时，是同一个类
                //1和3能并发，因为是不同的变量--泛型类，在变量参数不同时，是不同的类
            } 
            #endregion

            Console.WriteLine($"***************异步方法BtnTaskSafe结束 {Thread.CurrentThread.ManagedThreadId.ToString("00")} ***************");
            Console.WriteLine();

        }

        private static readonly object locks = new object();
        private static readonly string lockstring = "YANG";
    }
}
