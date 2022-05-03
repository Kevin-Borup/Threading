using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_VendingMachine
{
    internal class Program
    {
        private static int oneOverFifteen = 100 / 15;

        private static BottleQueue<string> bottles = new BottleQueue<string>(24);
        private static BottleQueue<string> beerBottles = new BottleQueue<string>(24);
        private static BottleQueue<string> sodaBottles = new BottleQueue<string>(24);

        static void Main(string[] args)
        {
            Thread bottleProducer = new Thread(BottleProducer) { Name = "Bottle Producer"};
            Thread bottleSplitter = new Thread(BottleSplitter) { Name = "Bottle Splitter"};
            Thread beerConsumer = new Thread(BeerConsumer) { Name = "Beer Consumer"};
            Thread sodaConsumer = new Thread(SodaConsumer) { Name = "Soda Consumer"};

            bottleProducer.Start();
            bottleSplitter.Start();
            beerConsumer.Start();
            sodaConsumer.Start();

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            bottleProducer.Join();
            bottleSplitter.Join();
            beerConsumer.Join();
            sodaConsumer.Join();

        }

        private static void BottleProducer()
        {
            Random numGen = new Random();
            int beerIncrement = 1;
            int sodaIncrement = 1;
            string bottle = string.Empty;

            while (true)
            {
                switch (numGen.Next(1, 3))
                {
                    case 1:
                        bottle = $"Beer-{beerIncrement}";
                        beerIncrement++;
                        break;
                    case 2:
                        bottle = $"Soda-{sodaIncrement}";
                        sodaIncrement++;
                        break;
                }

                while (true)
                {
                    if (Monitor.TryEnter(bottles.Lock))
                    {
                        if (bottles.Full)
                        {
                            Monitor.Wait(bottles.Lock);
                        }

                        bottles.Enqueue(bottle);
                        ConsoleWriter(bottle, bottles.Count);
                        Monitor.Pulse(bottles.Lock);
                        Monitor.Exit(bottles.Lock);
                        break;
                    }
                    Thread.Sleep(oneOverFifteen);
                }
            }
        }

        private static void BottleSplitter()
        {
            string bottle = string.Empty;
            while (true)
            {
                while (true)
                {
                    if (Monitor.TryEnter(bottles.Lock))
                    {
                        if (bottles.Empty)
                        {
                            Monitor.Wait(bottles.Lock);
                        }

                        bottle = bottles.Dequeue();
                        Monitor.Pulse(bottles.Lock);
                        Monitor.Exit(bottles.Lock);
                        break;
                    }
                    Thread.Sleep(oneOverFifteen);
                }

                if (bottle.Contains("Beer"))
                {
                    while (true)
                    {
                        if (Monitor.TryEnter(beerBottles.Lock))
                        {
                            if (beerBottles.Full)
                            {
                                Monitor.Wait(beerBottles.Lock);
                            }

                            beerBottles.Enqueue(bottle);
                            ConsoleWriter(bottle, beerBottles.Count);
                            Monitor.Pulse(beerBottles.Lock);
                            Monitor.Exit(beerBottles.Lock);
                            break;
                        }
                        Thread.Sleep(oneOverFifteen);
                    }
                }
                else if (bottle.Contains("Soda"))
                {
                    while (true)
                    {
                        if (Monitor.TryEnter(sodaBottles.Lock))
                        {
                            if (sodaBottles.Full)
                            {
                                Monitor.Wait(sodaBottles.Lock);
                            }

                            sodaBottles.Enqueue(bottle);
                            ConsoleWriter(bottle, sodaBottles.Count);
                            Monitor.Pulse(sodaBottles.Lock);
                            Monitor.Exit(sodaBottles.Lock);
                            break;
                        }
                        Thread.Sleep(oneOverFifteen);
                    }
                }
            }
        }

        private static void BeerConsumer()
        {
            while (true)
            {
                if (Monitor.TryEnter(beerBottles.Lock))
                {
                    if (beerBottles.Empty)
                    {
                        Monitor.Wait(beerBottles.Lock);
                    }

                    ConsoleWriter(beerBottles.Dequeue(), beerBottles.Count);
                    Monitor.Pulse(beerBottles.Lock);
                    Monitor.Exit(beerBottles.Lock);
                }
                Thread.Sleep(oneOverFifteen);
            }
        }

        private static void SodaConsumer()
        {
            while (true)
            {
                if (Monitor.TryEnter(sodaBottles.Lock))
                {
                    if (sodaBottles.Empty)
                    {
                        Monitor.Wait(sodaBottles.Lock);
                    }

                    ConsoleWriter(sodaBottles.Dequeue(), sodaBottles.Count);
                    Monitor.Pulse(sodaBottles.Lock);
                    Monitor.Exit(sodaBottles.Lock);
                }
                Thread.Sleep(oneOverFifteen);
            }
        }

        private static void ConsoleWriter(string bottle, int bottleCount)
        {
            string thread = Thread.CurrentThread.Name;
            switch (thread)
            {
                case "Bottle Producer":
                    Console.WriteLine(Thread.CurrentThread.Name + " - Produced one " + bottle + "\t\t\t\t\t| " + bottleCount + "/24");
                    break;
                case "Bottle Splitter":
                    string bottleType = bottle.Substring(0, 4);
                    Console.WriteLine(Thread.CurrentThread.Name + " - Sent a " + bottle + " to the " + bottleType + "belt" + " \t\t\t| " + bottleCount + "/24");
                    break;
                case "Beer Consumer":
                case "Soda Consumer":
                    string bottleAmount = bottle.Substring(5, bottle.Length - 5);
                    Console.WriteLine(Thread.CurrentThread.Name + " - Consumed " + bottle + " \t\t\t\t\t| Total: " + bottleAmount);
                    break;
            }
        }
    }
}
