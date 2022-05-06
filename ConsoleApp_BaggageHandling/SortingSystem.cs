using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class SortingSystem
    {
        FrontDesk desk1;
        FrontDesk desk2;
        FrontDesk desk3;

        BaggageLine<Baggage> sortingInventory;

        Terminal terminal1;
        Terminal terminal2;
        Terminal terminal3;

        Thread recievalThread;
        Thread deliveryThread;

        public SortingSystem(FrontDesk frontdesk1, FrontDesk frontdesk2, FrontDesk frontdesk3, BaggageLine<Baggage> sorting, Terminal term1, Terminal term2, Terminal term3)
        {
            desk1 = frontdesk1;
            desk2 = frontdesk2;
            desk3 = frontdesk3;

            sortingInventory = sorting;

            terminal1 = term1;
            terminal2 = term2;
            terminal3 = term3;

            recievalThread = new Thread(BaggageReciever) { Name = "Recieval Thread"};
            deliveryThread = new Thread(BaggageDeliverer) { Name = "Delivery Thread"};
        }

        public void RecievalSystemStarter()
        {
            recievalThread.Start();
        }

        public void RecievalSystemStopper()
        {
            recievalThread.Join();
        }

        public void DeliverySystemStarter()
        {
            deliveryThread.Start();
        }

        public void DeliverySystemStopper()
        {
            deliveryThread.Join();
        }

        #region Baggage Recieval System

        private void BaggageReciever()
        {

        }

        private Baggage DequeueDesk(Baggage providedBaggage, FrontDesk desk)
        {
            Baggage newBaggage = null;
            bool deskNotAccessed = true;

            while (deskNotAccessed)
            {
                if (Monitor.TryEnter(desk))
                {
                    if (!desk.BaggageEmpty())
                    {
                        desk.EnqueueBaggage(providedBaggage);
                        Debug.WriteLine(Thread.CurrentThread.Name + " - Pulled " + providedBaggage + " from Desk " + desk.ID);
                    }
                    deskNotAccessed = false;
                    Monitor.Exit(desk);
                }
            }

            return newBaggage;
        }

        #endregion

        #region Baggage Delivery System
        private void BaggageDeliverer()
        {

        }
        private bool EnqueueToTerminal(Baggage providedBaggage, Terminal term)
        {
            bool success = false;
            bool deskNotAccessed = true;

            while (deskNotAccessed)
            {
                if (Monitor.TryEnter(term))
                {
                    if (!term.BaggageFull())
                    {
                        term.EnqueueBaggage(providedBaggage);
                        Debug.WriteLine(Thread.CurrentThread.Name + " - Sorted " + providedBaggage + " to Terminal " + term.ID);
                        success = true;
                    }
                    deskNotAccessed = false;
                    Monitor.Exit(term);
                }
            }

            return success;
        }
        #endregion
    }
}
