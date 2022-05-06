using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_BaggageHandling
{
    internal class PlaneSystem
    {
        Terminal terminal1;
        Terminal terminal2;
        Terminal terminal3;

        public PlaneSystem(Terminal term1, Terminal term2, Terminal term3)
        {
            terminal1 = term1;
            terminal2 = term2;
            terminal3 = term3;
        }
    }
}
