using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_ProducerConsumer
{
    public class Exercise4
    {
        private static Queue<string> products = new Queue<string>();

        public Exercise4()
        {
            Main4();
        }

        public static void Main4()
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
                int tries = 0;
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
                        tries++;

                        if (tries == 7)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(100 / 15);
                }
                Thread.Sleep(2000);
            }
        }

        private static void Consume()
        {
            while (true)
            {
                int tries = 0;
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
                        tries++;

                        if (tries == 7)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(100 / 15);
                }
                Thread.Sleep(2000);
            }
        }
    }
}
