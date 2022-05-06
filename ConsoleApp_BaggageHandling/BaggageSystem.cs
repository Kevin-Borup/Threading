using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class BaggageSystem
    {
        int ticketNr = 0;
        int baggageNr = 0;

        FrontDesk desk1;
        FrontDesk desk2;
        FrontDesk desk3;

        Thread reservationThread;

        public BaggageSystem(FrontDesk frontdesk1, FrontDesk frontdesk2, FrontDesk frontdesk3)
        {
            desk1 = frontdesk1;
            desk2 = frontdesk2;
            desk3 = frontdesk3;

            reservationThread = new Thread(Generator) { Name = "Reservation Thread" };
        }

        public void SystemStarter()
        {
            reservationThread.Start();
        }

        public void SystemStopper()
        {
            reservationThread.Join();
        }

        private void Generator()
        {
            bool baggageToEnqueue;

            while (Program.applicationExecuting)
            {
                Baggage newBaggage = BaggageGenerator();
                baggageToEnqueue = true;

                while (baggageToEnqueue)
                {
                    bool desk1Success = false;
                    bool desk2Success = false;

                    desk1Success = EnqueueToDesk(newBaggage, desk1);
                    if (desk1Success)
                    {
                        baggageToEnqueue = false;
                    }

                    if (!desk1Success)
                    {
                        desk2Success = EnqueueToDesk(newBaggage, desk2);
                        baggageToEnqueue = false;
                    }

                    if (!desk1Success && !desk2Success)
                    {
                        EnqueueToDesk(newBaggage, desk3);
                        baggageToEnqueue = false;
                    }
                }
            }
        }

        private bool EnqueueToDesk(Baggage providedBaggage, FrontDesk desk)
        {
            bool success = false;
            bool deskNotAccessed = true;

            while (deskNotAccessed)
            {
                if (Monitor.TryEnter(desk))
                {
                    if (!desk.BaggageEmpty())
                    {
                        desk.EnqueueBaggage(providedBaggage);
                        Debug.WriteLine(Thread.CurrentThread.Name + " - Added " + providedBaggage + " to Desk " + desk.ID);
                        success = true;
                    }
                    deskNotAccessed = false;
                    Monitor.Exit(desk);
                }
            }

            return success;
        }

        private Ticket TicketGenerator()
        {
            Random random = new Random();

            string[] firstNames = new string[5] { "Mads", "Kevin", "Marius", "Mikkel", "Andreas" };
            string[] lastNames = new string[5] { "Edske", "Kim", "Jenner", "Kardashian", "Rømer" };
            string[] airports = new string[5] { "CPH", "AAL", "AAR", "EBJ", "SGD" };

            string name = firstNames[random.Next(0, 4)] + " " + lastNames[random.Next(0, 4)];
            string plane = airports[random.Next(0, 4)];

            return new Ticket(name, ticketNr++, plane);
        }

        private Baggage BaggageGenerator()
        {
            return new Baggage(TicketGenerator(), baggageNr++);
        }
    }
}
