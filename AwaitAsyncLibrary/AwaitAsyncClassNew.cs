using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsyncLibrary
{
    /// <summary>
    /// async/await:是个语法糖，出现在C#5.0 .Net Framework 4.5及以上（CLR4.0）
    /// 一个语法糖，不是一个全新的异步多线程使用方式
    /// （语法糖：就是编译器提供的新功能）
    /// 本身并不会产生先的线程，但依托于Task，所以程序执行时也是有多线程的
    /// async可以随便添加，可以不用await
    /// await必须出现在Task前，且方法必须实现声明async，不能单独出现
    /// </summary>
    public class AwaitAsyncClassNew
    {
        public async Task DoSomething()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("***************");
            });
        }


        public void Show()
        {
            Console.WriteLine($"This is Main strat {Thread.CurrentThread.ManagedThreadId}");
            //NoRetuen();
           AsyncBySync();
            //Console.WriteLine($"线程返回值{RetuenByAsync().GetAwaiter().GetResult()}"); ;
            Console.WriteLine($"This is Main end {Thread.CurrentThread.ManagedThreadId}");
        }

        private void NoRetuen()
        {
            Console.WriteLine($"This is NoRetuen strat {Thread.CurrentThread.ManagedThreadId}");
            Task.Run(() =>
            {
                Console.WriteLine($"This is NoRetuenTask strat {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(2000);
                Console.WriteLine($"This is NoRetuenTask end {Thread.CurrentThread.ManagedThreadId}");
            });
            Console.WriteLine($"This is NoRetuen end {Thread.CurrentThread.ManagedThreadId}");
        }

        private async Task NoRetuenByAsync()
        {
            Console.WriteLine($"This is NoRetuenByAsync strat {Thread.CurrentThread.ManagedThreadId}");//调用线程执行
            Task task = Task.Run(() =>
            {
                Console.WriteLine($"This is NoRetuenByAsyncTask strat {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
                Thread.Sleep(2000);
                Console.WriteLine($"This is NoRetuenByAsyncTask end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
            });//调用线程发起，启动新线程执行内部动作
            await task;//调用线程回去执行自己的内部动作
            Console.WriteLine($"This is NoRetuenByAsync end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行，若前面没有await，则由调用线程执行
            //上面两家代码，相当下面一句
            //task.ContinueWith(t => Console.WriteLine($"This is NoRetuenByAsync end {Thread.CurrentThread.ManagedThreadId}"));
            //加了await之后，相当于将await之后的代码，包装成一个回调----回调的线程具有多种可能性，可能是调用线程、子线程、新线程
        }

        private async Task<long> RetuenByAsync()
        {
            Console.WriteLine($"This is RetuenByAsync strat {Thread.CurrentThread.ManagedThreadId}");
            long result = 0;
            await Task<long>.Run(() =>
            {
                Console.WriteLine($"This is RetuenByAsyncTask strat {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 0; i < 10000000; i++)
                {
                    result += i;
                }
                Console.WriteLine($"This is RetuenByAsyncTask end {Thread.CurrentThread.ManagedThreadId}");
                return result;
            });
            Console.WriteLine($"This is RetuenByAsync end {Thread.CurrentThread.ManagedThreadId}");
            return result;
        }


        private async Task AsyncBySync()
        {
            Console.WriteLine($"This is AsyncBySync strat {Thread.CurrentThread.ManagedThreadId}");//调用线程执行
            Task task = Task.Run(() =>
            {
                Console.WriteLine($"This is AsyncBySyncTask1 strat {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
                Thread.Sleep(2000);
                Console.WriteLine($"This is AsyncBySyncTask1 end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
            });//调用线程发起，启动新线程执行内部动作
            await task;//调用线程回去执行自己的内部动作
            Console.WriteLine($"This is AsyncBySync_1 end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行，若前面没有await，则由调用线程执行


            await Task.Run(() =>
            {
                Console.WriteLine($"This is AsyncBySyncTask2 strat {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
                Thread.Sleep(2000);
                Console.WriteLine($"This is AsyncBySyncTask2 end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行
            });//调用线程发起，启动新线程执行内部动作
            Console.WriteLine($"This is AsyncBySync_2 end {Thread.CurrentThread.ManagedThreadId}");//Task子线程执行，若前面没有await，则由调用线程执行

            //如果有多个await,线程的执行顺序是可确定的
            //可以用同步编码的形式去写异步
        }

    }
}
