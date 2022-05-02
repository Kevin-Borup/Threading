using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_BasicThreadSync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Exercise1.Main1();
            Exercise2.Main2();
            //Exercise3.Main3();
            Console.Read();
        }
    }

    #region Øvelse1
    internal static class Exercise1
    {
        private static int counter = 0;
        private static object _lock = new object();

        public static void Main1()
        {
            Thread countUp = new Thread(ThreadCountUp);
            Thread countDown = new Thread(ThreadCountDown);

            countUp.Start();
            countDown.Start();

            while (true)
            {
                Thread.Sleep(1000);

                lock (_lock)
                {
                    Console.WriteLine(counter);
                }
            }
        }

        private static void ThreadCountUp()
        {
            while (true)
            {
                Thread.Sleep(1000);
                lock (_lock)
                {
                    counter += 2;
                }
            }
        }

        private static void ThreadCountDown()
        {
            while (true)
            {
                Thread.Sleep(1000);
                lock (_lock)
                {
                    counter--;
                }
            }
            
        }
    }
    #endregion

    #region Øvelse2
    internal static class Exercise2
    {
        private static int sum = 0;
        private static object _lock = new object();
        private static bool running = true;


        public static void Main2()
        {
            Thread starThread = new Thread(StarPrinter);
            Thread hashtagThread = new Thread(HashtagPrinter);

            starThread.Start();
            hashtagThread.Start();

            while (running)
            {
                Thread.Sleep(500);
                lock (_lock)
                {
                    if (sum >= 600)
                    {
                        running = false;
                    }
                }
                
            }

            starThread.Join();
            hashtagThread.Join();

        }

        private static void StarPrinter()
        {
            string star = new string('*', 60);

            while (running)
            {
                Thread.Sleep(500);
                lock (_lock)
                {
                    sum += 60;
                    Console.WriteLine(star + " " + sum);
                }
            }
        }

        private static void HashtagPrinter()
        {
            string hashtag = new string('#', 60);

            while (running)
            {
                Thread.Sleep(500);
                lock (_lock)
                {
                    sum += 60;
                    Console.WriteLine(hashtag + " " + sum);

                }
            }

        }
    }
    #endregion

    #region Øvelse3
    // Dette burde være løst i opgave 2
    internal static class Exercise3
    {
        public static void Main3()
        {

        }
    }
    #endregion
}

