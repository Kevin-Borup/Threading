using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_ProducerConsumer
{
    internal class ExerciseMutex
    {
        private static Mutex mutex = new Mutex(false);
        private static Queue<string> products = new Queue<string>();
        public ExerciseMutex()
        {
            MainMutex();
        }

        public static void MainMutex()
        {
            Thread producer = new Thread(Produce);
            Thread consumer = new Thread(Consume);

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();
        }

        private static void Produce()
        {
            while (true)
            {
                Console.WriteLine("Producer waits ...");
                mutex.WaitOne();
                while (products.Count < 3)
                {
                    products.Enqueue("product");
                    Console.WriteLine("Producer har produceret: Nr. " + products.Count);
                }
                mutex.ReleaseMutex();
                Thread.Sleep(100 / 15);
            }

        }

        private static void Consume()
        {
            while (true)
            {
                Console.WriteLine("Consumer waits ...");
                mutex.WaitOne();
                while (products.Count > 0)
                {
                    products.Dequeue();
                    Console.WriteLine("Consumer har consumeret: Nr " + (products.Count + 1));
                }
                mutex.ReleaseMutex();
                Thread.Sleep(100 / 15);
            }

        }
    }
}
