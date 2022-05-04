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
        private static BottleQueue<Bottle> producedBottles = new BottleQueue<Bottle>(24);
        private static BottleQueue<Bottle> beerBottles = new BottleQueue<Bottle>(24);
        private static BottleQueue<Bottle> sodaBottles = new BottleQueue<Bottle>(24);

        static void Main(string[] args)
        {
            BottleProducer producer = new BottleProducer(producedBottles);
            BottleSplitter splitter = new BottleSplitter(producedBottles, beerBottles, sodaBottles);
            BottleConsumer beerExport = new BottleConsumer(beerBottles);
            BottleConsumer sodaExport = new BottleConsumer(sodaBottles);

            Thread bottleProducer = new Thread(producer.Produce) { Name = "Bottle Producer"};
            Thread bottleSplitter = new Thread(splitter.Split) { Name = "Bottle Splitter"};
            Thread beerConsumer = new Thread(beerExport.Consume) { Name = "Beer Consumer"};
            Thread sodaConsumer = new Thread(sodaExport.Consume) { Name = "Soda Consumer"};

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

        /// <summary>
        /// Writes the relevant line to the Console, regarding a status update on the current Thread.
        /// <para>Bottle and BottleCount is used to add further data to the update.</para>
        /// </summary>
        /// <param name="bottle"></param>
        /// <param name="bottleCount"></param>
        public static void ConsoleWriter(Bottle bottle, int bottleCount)
        {
            string thread = Thread.CurrentThread.Name;
            switch (thread)
            {
                case "Bottle Producer":
                    Console.WriteLine($"{Thread.CurrentThread.Name} - Produced one {bottle}\t\t\t\t\t| {bottleCount}/24");
                    break;
                case "Bottle Splitter":
                    Console.WriteLine($"{Thread.CurrentThread.Name}  - Sent a {bottle} to the {bottle.Type}belt \t\t\t| {bottleCount}/24");
                    break;
                case "Beer Consumer":
                case "Soda Consumer":
                    Console.WriteLine($"{Thread.CurrentThread.Name} - Consumed {bottle} \t\t\t\t\t| Total: {bottle.ID}");
                    break;
            }
        }

        /// <summary>
        /// Writes exceptions to the Console in a readable manner.
        /// </summary>
        /// <param name="e"></param>
        public static void ExceptionWriter(Exception e)
        {
            Console.WriteLine("An Exception occured: " + e.Message + " from " + e.Source + "\n" + e);
        } 
    }
}
