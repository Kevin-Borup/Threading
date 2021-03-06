using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_ProducerConsumer
{
    internal class Exercise5
    {
        private static object _lock = new object();
        private static Queue<string> products = new Queue<string>();

        public Exercise5()
        {
            Main5();
        }
        public static void Main5()
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
                Monitor.Enter(_lock);
                if (products.Count < 3)
                {
                    products.Enqueue("product");
                    Console.WriteLine("Producer har produceret: Nr. " + products.Count);
                }
                else
                {
                    Monitor.Pulse(_lock);
                    Console.WriteLine("Producer waits ...");
                    Monitor.Wait(_lock);
                }
                Monitor.Exit(_lock);
                Thread.Sleep(100 / 15);
            }
        }

        private static void Consume()
        {
            while (true)
            {
                Monitor.Enter(_lock);
                if (products.Count > 0)
                {
                    products.Dequeue();
                    Console.WriteLine("Consumer har consumeret: Nr " + (products.Count + 1));
                }
                else
                {
                    Monitor.Pulse(_lock);
                    Console.WriteLine("Consumer waits ...");
                    Monitor.Wait(_lock);
                }
                Monitor.Exit(_lock);
                Thread.Sleep(100 / 15);
            }
        }
    }
}
