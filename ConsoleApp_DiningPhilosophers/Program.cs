using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_DiningPhilosophers
{
    internal class Program
    {
        static bool continueExecuting = true;
        static object[] forks = new object[5] {new object(), new object(), new object(), new object(), new object()};

        static void Main(string[] args)
        {
            Thread p1 = new Thread(EatSpaghetti1);
            Thread p2 = new Thread(EatSpaghetti1);
            Thread p3 = new Thread(EatSpaghetti1);
            Thread p4 = new Thread(EatSpaghetti1);
            Thread p5 = new Thread(EatSpaghetti1);

            p1.Name = "2";
            p2.Name = "3";
            p3.Name = "4";
            p4.Name = "5";
            p5.Name = "6";

            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();
            p5.Start();

            while (continueExecuting)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    continueExecuting = false;
                }
            }

            p1.Join();
            p2.Join();
            p3.Join();
            p4.Join();
            p5.Join();

        }

        private static void EatSpaghetti1()
        {
            Random numGen = new Random();

            while (continueExecuting)
            {
                int threadNum = Convert.ToInt32(Thread.CurrentThread.Name);

                int leftFork = threadNum - 2;
                int rightFork = threadNum - 3;

                if (rightFork < 0)
                {
                    rightFork = 4;
                }

                while (continueExecuting)
                {
                    if (threadNum == 6)
                    {
                        Console.WriteLine("Philosopher " + Thread.CurrentThread.Name + " is waiting...");
                        Thread.Sleep(50);
                        Monitor.Enter(forks[rightFork]);
                        Monitor.Enter(forks[leftFork]);
                        Console.WriteLine(" --- Philosopher " + Thread.CurrentThread.Name + " is eating...");
                        Thread.Sleep(numGen.Next(100, 350));
                        Console.WriteLine("Philosopher " + Thread.CurrentThread.Name + " is thinking...");
                        Monitor.Exit(forks[leftFork]);
                        Monitor.Exit(forks[rightFork]);
                    }
                    else
                    {
                        Console.WriteLine("Philosopher " + Thread.CurrentThread.Name + " is waiting...");
                        Thread.Sleep(50);
                        Monitor.Enter(forks[leftFork]);
                        Monitor.Enter(forks[rightFork]);
                        Console.WriteLine(" --- Philosopher " + Thread.CurrentThread.Name + " is eating...");
                        Thread.Sleep(numGen.Next(100, 350));
                        Console.WriteLine("Philosopher " + Thread.CurrentThread.Name + " is thinking...");
                        Monitor.Exit(forks[rightFork]);
                        Monitor.Exit(forks[leftFork]);
                    }
                }
            }
        }
    }
}
