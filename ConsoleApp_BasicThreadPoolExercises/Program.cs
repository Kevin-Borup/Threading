using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_BasicThreadPoolExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Exercise0.Main0();
            //Exercise1.Main1();
            //Exercise2a.Main2a();
            //Exercise2b.Main2b();
            Exercise3.Main3();
            Console.Read();
        }
    }

    #region Øvelse0

    // Forskellen:
    // Med WaitCallBack så venter funktionen til den er færdiggjort med et loop før det bliver kaldt igen. Uden det, så kører den første 6 gange, før den anden er begyndt.
    internal class Exercise0
    {
        public static void Main0()
        {
            Exercise0 ex0 = new Exercise0();

            for (int i = 0; i < 2; i++)
            {
                ThreadPool.QueueUserWorkItem(ex0.Task1);
                ThreadPool.QueueUserWorkItem(ex0.Task2);
            }
        }

        public void Task1(object obj)
        {
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("Task 1 is being executed");
            }
        }

        public void Task2(object obj)
        {
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("Task 2 is being executed");
            }
        }
    }
    #endregion

    #region Øvelse1

    // Process skal tage et object som argument, da det er et krav for at funktionen kan blive kaldt med ThreadPool.QueueUserWorkItem();
    // Den skal bruge dette for at konvertere den fra en method til en CallBack.

    // Thread Pool Execution
    // Time consumed by ProcessWithThreadPoolMethod is : 358441
    // Thread Execution
    // Time consumed by ProcessWithThreadMethod is : 941296
    internal static class Exercise1
    {
        public static void Main1()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Thread Pool Execution");
            stopwatch.Start();
            ProcessWithThreadPoolMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is : " + stopwatch.ElapsedTicks.ToString());
            stopwatch.Reset();

            Console.WriteLine("Thread Execution");
            stopwatch.Start();
            ProcessWithThreadMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadMethod is : " + stopwatch.ElapsedTicks.ToString());

        }

        static void ProcessWithThreadMethod()
        {
            for (int i = 0; i <= 10; i++)
            {
                Thread obj = new Thread(Process);
                obj.Start();
            }
        }

        static void ProcessWithThreadPoolMethod()
        {
            for (int i = 0; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Process);
            }
        }

        static void Process(object callback)
        {

        }
    }
    #endregion

    #region Øvelse2a
    // Eksekveringstiden bliver til en kæmpe forskel da den stiger eksponentielt
    internal static class Exercise2a
    {
        public static void Main2a()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Thread Pool Execution");
            stopwatch.Start();
            ProcessWithThreadPoolMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is : " + stopwatch.ElapsedTicks.ToString());
            stopwatch.Reset();

            Console.WriteLine("Thread Execution");
            stopwatch.Start();
            ProcessWithThreadMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadMethod is : " + stopwatch.ElapsedTicks.ToString());

        }

        static void ProcessWithThreadMethod()
        {
            for (int i = 0; i <= 100000; i++)
            {
                Thread obj = new Thread(Process);
                obj.Start();
            }
        }

        static void ProcessWithThreadPoolMethod()
        {
            for (int i = 0; i <= 100000; i++)
            {
                ThreadPool.QueueUserWorkItem(Process);
            }
        }

        static void Process(object callback)
        {

        }
    }
    #endregion

    #region Øvelse2b
    internal static class Exercise2b
    {
        public static void Main2b()
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Thread Pool Execution");
            stopwatch.Start();
            ProcessWithThreadPoolMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadPoolMethod is : " + stopwatch.ElapsedTicks.ToString());
            stopwatch.Reset();

            Console.WriteLine("Thread Execution");
            stopwatch.Start();
            ProcessWithThreadMethod();
            stopwatch.Stop();

            Console.WriteLine("Time consumed by ProcessWithThreadMethod is : " + stopwatch.ElapsedTicks.ToString());

        }

        static void ProcessWithThreadMethod()
        {
            for (int i = 0; i <= 100000; i++)
            {
                for (int j = 0; j < 100000; j++)
                {
                    Thread obj = new Thread(Process);
                    obj.Start();
                }
            }
        }

        static void ProcessWithThreadPoolMethod()
        {
            for (int i = 0; i <= 100000; i++)
            {
                for (int j = 0; j < 100000; j++)
                {
                    ThreadPool.QueueUserWorkItem(Process);
                }
            }
        }

        static void Process(object callback)
        {

        }
    }
    #endregion

    #region Øvelse3
    internal static class Exercise3
    {
        public static void Main3()
        {
            while (true)
            {
                ThreadPool.QueueUserWorkItem(TestThreadProperties);
            }
        }

        internal static void TestThreadProperties(object callback)
        {
            Console.WriteLine("Current Thread IsAlive: " + Thread.CurrentThread.IsAlive);
            Console.WriteLine("Current Thread IsBackground: " + Thread.CurrentThread.IsBackground);
            Console.WriteLine("Current Thread Priority: " + Thread.CurrentThread.Priority);

            Random numGen = new Random();
            int randomNum = numGen.Next(0, 5);
            Console.WriteLine(randomNum);
            switch (randomNum)
            {
                case 0:
                    TestThreadStart();
                    break;
                case 1:
                    TestThreadSleep();
                    break;
                case 2:
                    TestThreadSuspend();
                    break;
                case 3:
                    TestThreadResume();
                    break;
                case 4:
                    TestThreadAbort();
                    break;
                case 5:
                    TestThreadJoin();
                    break;
            }
        }

        internal static void TestThreadStart()
        {
            Thread.CurrentThread.Start();
        }

        internal static void TestThreadSleep()
        {
            Thread.Sleep(1000);
        }

        internal static void TestThreadSuspend()
        {
            Thread.CurrentThread.Suspend();
        }

        internal static void TestThreadResume()
        {
            Thread.CurrentThread.Resume();
        }

        internal static void TestThreadAbort()
        {

            Thread.CurrentThread.Abort();
        }

        internal static void TestThreadJoin()
        {
            Thread.CurrentThread.Join();
        }
    }
    #endregion
}
