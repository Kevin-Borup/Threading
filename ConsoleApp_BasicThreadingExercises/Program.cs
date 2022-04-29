using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_BasicThreadingExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Exercise0.Main0();
            //Exercise1.Main1();
            //Exercise2.Main2();
            //Exercise3.Main3();
            //Exercise4.Main4(); 
            Console.Read();
        }
    }

    #region Øvelse0
    internal static class Exercise0
    {
        public static void Main0()
        {
            program0 pg = new program0();
            //Her opretter vi de 2 threads, på den samme funktion som nu kører i 2 forskellige threads-
            Thread thread1 = new Thread(new ThreadStart(pg.WorkThreadFunction));
            Thread thread2 = new Thread(new ThreadStart(pg.WorkThreadFunction));
            //Navngivningen er med til at definere hvad de skriver
            thread1.Name = "Simple Thread";
            thread2.Name = "Second Thread";
            //Her bliver de startet
            thread1.Start();
            thread2.Start();
        }

    }

    internal class program0
    {
        /// <summary>
        /// Denne funktion skriver thread navnet hver halve sekund
        /// </summary>
        public void WorkThreadFunction()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }
    }
    #endregion

    #region Øvelse1
    internal class Exercise1
    {
        public static void Main1()
        {
            Thread writerThread = new Thread(Printer);
            writerThread.Start();

        }

        private static void Printer()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("C#-trådning er nemt!");
            }
        }
    }
    #endregion

    #region Øvelse2
    internal class Exercise2
    {
        public static void Main2()
        {
            Thread writerThread1 = new Thread(Printer1);
            Thread writerThread2 = new Thread(Printer2);
            writerThread1.Start();
            writerThread2.Start();

        }

        private static void Printer1()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("C#-trådning er nemt!");
            }
        }
        private static void Printer2()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Også med flere tråde ...");
            }
        }
    }
    #endregion

    #region Øvelse3
    internal class Exercise3
    {
        internal static bool TempGen = true;
        internal static int Alarms = 0;
        internal static int Temperature = 50;

        public static void Main3()
        {
            Thread tempGen = new Thread(TemperatureGenerator);
            Thread tempAlarm = new Thread(TemperatureAlarm);
            tempGen.Start();
            tempAlarm.Start();


            while (true)
            {
                Thread.Sleep(10000);
                if (!tempAlarm.IsAlive)
                {
                    Console.WriteLine("Alarm-tråd termineret!");
                    TempGen = false;
                    tempGen.Join();
                    return;
                }
            }
        }

        private static void TemperatureGenerator()
        {
            while (TempGen)
            {
                Random randomNumGen = new Random();
                Temperature = randomNumGen.Next(-20, 120);
                Console.WriteLine("Current Temperature: " + Temperature);
                Thread.Sleep(2000);
            }
        }

        private static void TemperatureAlarm()
        {
            while (true)
            {
                if (Temperature < 0 || 100 < Temperature)
                {
                    Console.WriteLine("Alarm!");
                    Alarms++;
                    
                }

                if (Alarms >= 3)
                {
                    return;
                }

                Thread.Sleep(2000);

            }
        }
    }
    #endregion

    #region Øvelse4
    internal class Exercise4
    {
        internal static char ch = '*';

        public static void Main4()
        {
            Console.WriteLine("Terminate the program with Ctrl-C");
            
            Thread printer = new Thread(Printer);
            Thread reader = new Thread(Reader);

            printer.Start();
            reader.Start();
        }

        internal static void Printer()
        {
            while (true)
            {
                Console.Write(ch);
                Thread.Sleep(100);
            }
        }

        internal static void Reader()
        {
            while (true)
            {
                char chTemp = Console.ReadKey().KeyChar;
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ch = chTemp;
                    Console.WriteLine();
                }
            }
        }
    }
    #endregion
}
