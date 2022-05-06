using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class Terminal : Transput
    {
        public Terminal(int id, int baggageLineSize) : base(id, baggageLineSize)
        {
        }

        public void EnqueueBaggage(Baggage recievedBaggage)
        {
            recievedBaggage.SetSortedTime();
            baggages.Enqueue(recievedBaggage);
        }
    }
}
