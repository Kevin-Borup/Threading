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
                if (products.Count < 3)
                {
                    products.Enqueue("product");
                    Console.WriteLine("Producer har produceret: Nr. " + products.Count);
                }
                else
                {
                    Console.WriteLine("Producer fik ikke lov til at producere: Nr. " + products.Count);
                }
            }
        }

        private static void Consume()
        {
            while (true)
            {
                if (products.Count > 0)
                {
                    products.Dequeue();
                    Console.WriteLine("Consumer har consumeret: Nr " + (products.Count + 1));
                }
                else
                {
                    Console.WriteLine("Consumer fik ikke lov til at consumere: Nr. " + products.Count);
                }
            }
        }
    }
}
