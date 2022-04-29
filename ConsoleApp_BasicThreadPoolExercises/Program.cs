using System;
using System.Collections.Generic;
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
            Exercise0.Main0();
            //Exercise1.Main1();
            //Exercise2.Main2();
            //Exercise3.Main3();
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
    internal static class Exercise1
    {
        public static void Main1()
        {
        }
    }
    #endregion

    #region Øvelse2
    internal static class Exercise2
    {
        public static void Main2()
        {
        }
    }
    #endregion

    #region Øvelse3
    internal static class Exercise3
    {
        public static void Main3()
        {
        }
    }
    #endregion
}
