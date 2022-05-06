using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class Ticket
    {
        public string PassengerName { get; private set; }
        public int PassengerNr { get; private set; }
        public string Departure { get; private set; }

        public Ticket(string name, int nr, string plane)
        {
            PassengerName = name;
            PassengerNr = nr;
            Departure = plane;
        }
    }
}
