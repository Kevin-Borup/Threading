using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class FrontDesk : Transput
    {
        public FrontDesk(int id, int baggageLineSize) : base(id, baggageLineSize)
        {
        }

        public void EnqueueBaggage(Baggage recievedBaggage)
        {
            recievedBaggage.SetRecievedTime();
            baggages.Enqueue(recievedBaggage);
        }

    }
}