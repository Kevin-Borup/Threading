using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class Baggage
    {
        public Ticket TicketRef { get; private set; }
        public int BaggageNr { get; private set; }
        public DateTime Recieved { get; private set; }
        public DateTime Sorted { get; private set; }

        public Baggage(Ticket providedTicket, int nr)
        {
            TicketRef = providedTicket;
            BaggageNr = nr;
        }

        public void SetRecievedTime()
        {
            Recieved = DateTime.Now;
        }

        public void SetSortedTime()
        {
            Sorted = DateTime.Now;
        }

        public override string ToString()
        {
            return "Baggage" + BaggageNr;
        }
    }
}
