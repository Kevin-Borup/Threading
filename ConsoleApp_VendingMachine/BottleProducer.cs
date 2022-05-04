using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_VendingMachine
{
    /// <summary>
    /// This class maintains the methods needed to create a running producer of Bottle classes to the appropriate Queue.
    /// <para>It waits if productionQueue is full</para>
    /// </summary>
    internal class BottleProducer
    {
        private BottleQueue<Bottle> producedBottles;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bottles"></param>
        public BottleProducer(BottleQueue<Bottle> bottles)
        {
            producedBottles = bottles;
        }

        /// <summary>
        /// Produces a bottle object with randomized type. The bottle is then added to the Queue recieved with the Constructor.
        /// </summary>
        public void Produce()
        {
            Random numGen = new Random();
            int beerIncrement = 1;
            int sodaIncrement = 1;
            Bottle bottle;

            while (true)
            {
                if (numGen.Next(1, 3) == 1)
                {
                    bottle = new Bottle("Beer", beerIncrement);
                    beerIncrement++;
                }
                else
                {
                    bottle = new Bottle("Soda", sodaIncrement);
                    sodaIncrement++;
                }

                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(producedBottles.Lock))
                        {
                            if (producedBottles.Full)
                            {
                                //Wait for the lock if the queue is full to avoid ressource waste.
                                Monitor.Wait(producedBottles.Lock);
                            }

                            producedBottles.Enqueue(bottle);
                            Program.ConsoleWriter(bottle, producedBottles.Count);
                            Monitor.Pulse(producedBottles.Lock);
                            Monitor.Exit(producedBottles.Lock);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        if (Monitor.IsEntered(producedBottles.Lock))
                        {
                            Monitor.Exit(producedBottles.Lock);
                        }
                        Program.ExceptionWriter(e);
                    }
                }
            }
        }
    }
}
