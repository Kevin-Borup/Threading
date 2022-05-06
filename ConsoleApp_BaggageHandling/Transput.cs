using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class Transput
    {
        public int ID { get; private set; }
        public bool Open { get; private set; }
        protected BaggageLine<Baggage> baggages;
        public Transput(int id, int baggageLineSize)
        {
            ID = id;
            Open = true;
            baggages = new BaggageLine<Baggage>(baggageLineSize);
        }

        public bool BaggageEmpty()
        {
            return baggages.Empty;
        }

        public bool BaggageFull()
        {
            return baggages.Full;
        }

        public dynamic DequeueBaggage()
        {
            return baggages.Dequeue();
        }
    }
}
