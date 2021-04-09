using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sync_Async_Demo
{
    public class TestLock
    {
        public static readonly object locks=new object();

        private readonly object locketmp = new object();

        private readonly string lockstring = "YANG";
        public static void Show()
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (locks)
                    {
                        Console.WriteLine($"This is TestLock {i} {k} run start {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(2000);
                        Console.WriteLine($"This is TestLock {i} {k} run end {Thread.CurrentThread.ManagedThreadId}");
                    }
                });
            }
        }

        public void ShowTmp(int index)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (locketmp)
                    {
                        Console.WriteLine($"This is ShowTmp {i} {k} run start{index} {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(2000);
                        Console.WriteLine($"This is ShowTmp {i} {k} run end{index} {Thread.CurrentThread.ManagedThreadId}");
                    }
                });
            }
        }

        public void ShowString()
        {
            GCHandle handle = GCHandle.Alloc(lockstring); 
            var pin = GCHandle.ToIntPtr(handle);
            Console.WriteLine(pin);
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (lockstring)
                    {
                        Console.WriteLine($"This is ShowString {i} {k} run start {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(2000);
                        Console.WriteLine($"This is ShowString {i} {k} run end {Thread.CurrentThread.ManagedThreadId}");
                    }
                });
            }
        }
    }

    public class TestLockGeneric<T>
    {
        public static readonly object locks = new object();

        public static void Show(int index)
        {
            for (int i = 0; i < 5; i++)
            {
                int k = i;
                Task.Run(() =>
                {
                    lock (locks)
                    {
                        Console.WriteLine($"This is TestLockGeneric {i} {k} run start{index} {Thread.CurrentThread.ManagedThreadId}");
                        Thread.Sleep(2000);
                        Console.WriteLine($"This is TestLockGeneric {i} {k} run end{index} {Thread.CurrentThread.ManagedThreadId}");
                    }
                });
            }
        }
    }
    }
